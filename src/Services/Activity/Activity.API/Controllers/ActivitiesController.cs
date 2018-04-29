using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Together.Activity.API.Applications.Commands;
using Together.Activity.API.Applications.Models;
using Together.Activity.API.Applications.Queries;
using Together.Activity.API.Models;
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
        public async Task<IActionResult> CreateActivity()
        {
            var result = await _mediator.Send(new CreateActivityCommand(currentUser, "测试活动", "test", DateTime.Now, DateTime.Now, "Beijing, China", 10));
            return result ? Ok() : (IActionResult)BadRequest();
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
        public async Task<IActionResult> GetActivities()
        {
            var activities = await _activityQueries.GetActivitiesAsync();
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
                await _mediator.Send(new JoinActivityCommand(activityId, currentUser));
                return Ok();
            }
            catch (ActivityDomainException)
            {
                return BadRequest("活动加入人数已满");
            }
        }
    }
}
