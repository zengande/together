using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Models;
using Together.Activity.API.Applications.Queries;
using Together.Activity.Domain.Exceptions;

namespace Together.Activity.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class ActivitiesController : BaseController
    {
        private IMediator _mediator;
        private IActivityQueries _activityQueries;
        public ActivitiesController(IMediator mediator,
            IActivityQueries activityQueries)
        {
            _mediator = mediator;
            _activityQueries = activityQueries;
        }

        [Route("create_activity")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateActivity(CreateActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var address = new Domain.AggregatesModel.ActivityAggregate.Address("ZheJiang", "HangZhou", "XiHu", "浙大科技园","");
                    var result = await _mediator.Send(new CreateActivityCommand(CurrentUser, model.Title, model.Details, model.EndRegisterDate, model.ActivitDate, model.StartTime, model.EndTime, address, model.LimitsNum));
                    return result ? Ok() : (IActionResult)BadRequest();
                }
                catch (ActivityDomainException ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest();
        }

        [Route("{activityId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(ActivityViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int activityId)
        {
            try
            {
                var activity = await _activityQueries.GetActivityAsync(activityId);
                return Ok(activity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActivitySummaryViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetActivities(int? pageIndex = 1, int? pageSize = 10)
        {
            pageIndex = pageIndex.HasValue ? (pageIndex.Value <= 0 ? 1 : pageIndex.Value) : 1;
            pageSize = pageSize.HasValue ? (pageSize.Value <= 0 ? 10 : pageSize.Value) : 10;
            var activities = await _activityQueries.GetActivitiesAsync(pageIndex.Value, pageSize.Value);
            return Ok(activities);
        }

        [Route("join")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> JoinActivity([FromBody]int activityId)
        {
            try
            {
                await _mediator.Send(new JoinActivityCommand(activityId, CurrentUser));
                return Ok();
            }
            catch (ActivityDomainException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest($"活动不存在");
            }
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
