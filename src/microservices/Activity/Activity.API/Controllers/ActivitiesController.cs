using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.Application.Commands;
using Together.Activity.Application.Dtos;
using Together.Activity.Application.Queries;
using Together.Activity.Domain.AggregatesModel.ActivityAggregate;
using Together.BuildingBlocks.Infrastructure.Identity;

namespace Together.Activity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [OpenApiTag("活动", Description = "获取、创建、加入活动")]
    public class ActivitiesController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly IActivityQueries _queries;
        public ActivitiesController(IMediator mediator,
            IIdentityService identityService,
            IActivityQueries queries)
        {
            _queries = queries;
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet, Route("{activityId}")]
        public async Task<IActionResult> Get(int activityId)
        {
            var userId = _identityService.GetUserIdentity();
            var activity = await _queries.GetActivityByIdAsync(activityId, userId);
            return activity != null ? Ok(activity) : (IActionResult)NotFound(activityId);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateActivityDto dto, [FromHeader(Name = "x-requestid")] string requestId)
        {
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var userId = _identityService.GetUserIdentity();
                // TODO 获取用户信息
                var creator = new Attendee(userId, "nickname", "", 1, true);
                var address = new Address("杭州", "西湖区", "西湖风景区", 0, 0);
                var command = new CreateActivityCommand(creator, dto.Title, dto.Content, dto.EndRegisterTime, dto.ActivityStartTime, dto.ActivityEndTime, address, dto.CategoryId, dto.AddressVisibleRuleId, dto.LimitsNum);
                var requestCreateActivity = new IdentifiedCommand<CreateActivityCommand, int>(command, guid);
                var activityId = await _mediator.Send(requestCreateActivity);
                if (activityId > 0)
                {
                    return Created(Url.Action(nameof(ActivitiesController.Get), new { activityId }), activityId);
                }
            }

            return BadRequest();
        }

        [HttpGet, Route("{activityId}/attendees")]
        public async Task<IActionResult> GetActivityAttendeesAsync(int activityId)
        {
            var attendees = await _queries.GetActivityAttendeesAsync(activityId);
            return Ok(attendees);
        }

        [Authorize]
        [HttpPost, Route("{activityId}/join")]
        public async Task<IActionResult> JoinAsync(int activityId, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var userInfo = _identityService.GetUserInfo();
                var command = new JoinActivityCommand(activityId, userInfo);
                var requestJoinActivity = new IdentifiedCommand<JoinActivityCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestJoinActivity);
                //commandResult = await _mediator.Send(command);
            }

            return commandResult ? Ok() : (IActionResult)BadRequest();
        }

        [Authorize]
        [HttpPost, Route("{activityId}/collect")]
        public async Task<IActionResult> CollectAsync(int activityId, [FromHeader(Name = "x-requestid")] string requestId)
        {
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty)
            {
                var userId = _identityService.GetUserIdentity();
                var command = new CollectActivityCommand(activityId, userId);
                var requestJoinActivity = new IdentifiedCommand<CollectActivityCommand, bool>(command, guid);
                commandResult = await _mediator.Send(requestJoinActivity);
            }

            return commandResult ? Ok() : (IActionResult)BadRequest();
        }
    }
}
