using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Services;
using Together.UserGroup.API.Models;

namespace Together.UserGroup.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class GroupsController
        : ControllerBase
    {
        private readonly IGroupService _groupService;
        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [Route("{groupId:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int groupId)
        {
            var group = await _groupService.GetAsync(groupId);
            if (group != null)
            {
                return Ok(group);
            }
            return NotFound();
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Post(InputGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                var group = await _groupService.CreateGroup(model.ToEntity());
                if (group != null)
                {
                    return group.Id != 0 ? Ok(group) : (IActionResult)BadRequest();
                }
                return BadRequest("名字已被占用");
            }
            return BadRequest();
        }
    }
}
