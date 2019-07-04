using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Applications.Dtos;
using Together.Activity.API.Applications.Queries;

namespace Together.Activity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    [ApiController]
    public class ActivitiesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IActivityQueries _activityQueries;
        private readonly ILogger<ActivitiesController> _logger;
        public ActivitiesController(IMediator mediator,
            IActivityQueries activityQueries,
            ILogger<ActivitiesController> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _activityQueries = activityQueries;
            _mapper = mapper;
        }

        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody]CreateActivityCommand command, [FromHeader(Name = "x-requestid")] string requestId)
        {
            _logger.LogInformation($"创建活动：{JsonConvert.SerializeObject(command)}");
            bool commandResult = false;
            if (Guid.TryParse(requestId, out Guid guid) &&
                guid != Guid.Empty)
            {
                command.Owner = CurrentUser;
                var request = new IdentifiedCommand<CreateActivityCommand, bool>(command, guid);
                commandResult = await _mediator.Send(request);
            }

            return commandResult ? Ok() : (IActionResult)BadRequest();
        }


        [Route("{activityId:int}")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ActivityDetailDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int activityId)
        {
            try
            {
                var activity = await _activityQueries.GetActivityAsync(activityId);
                activity.IsJoined = await _activityQueries.AlreadyJoined(activityId, CurrentUser?.UserId);
                return Ok(activity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ActivitySummaryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActivities(int? pageIndex = 1, int? pageSize = 10)
        {
            pageIndex = pageIndex.HasValue ? (pageIndex.Value <= 0 ? 1 : pageIndex.Value) : 1;
            pageSize = pageSize.HasValue ? (pageSize.Value <= 0 ? 10 : pageSize.Value) : 10;
            var activities = await _activityQueries.GetActivitiesAsync(pageIndex.Value, pageSize.Value);
            return Ok(activities);
        }

        [Route("nearby")]
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ActivitySummaryDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Nearby(string location)
        {
            var activities = await _activityQueries.GetLatestActivitiesNearby(1, 10, null);
            return Ok(activities);
        }

        [Route("join/{activityId:int}")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> JoinActivity(int activityId, [FromHeader(Name = "x-requestid")]string requestId)
        {
            if (!Guid.TryParse(requestId, out var guid) ||
                guid == Guid.Empty)
            {
                return BadRequest();
            }
            var command = new IdentifiedCommand<JoinActivityCommand, bool>(new JoinActivityCommand(activityId, CurrentUser), guid);
            var result = await _mediator.Send(command);
            return result ? Ok() : (IActionResult)BadRequest();
        }

        [Route("joined_activities")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMyJoinedActivities()
        {
            var activities = await _activityQueries.GetActivitiesByUserAsync(CurrentUser.UserId);
            return Ok(activities);
        }

    }
}
