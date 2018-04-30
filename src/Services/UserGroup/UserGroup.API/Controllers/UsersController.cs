using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Together.UserGroup.API.Infrastructure.Services;
using Together.UserGroup.API.Models;

namespace Together.UserGroup.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("{userId:int}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _userService.GetAsync(userId);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody]InputUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AddAsync(model.ToEntity());
                var result = await _userService.SaveChangesAsync();
                return result ? Ok(user) : (IActionResult)BadRequest();
            }
            return BadRequest();
        }

        [Route("join_group")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> JoinGroup(int groupId)
        {
            var user = await _userService.JoinGroup(CurrentUser.UserId, groupId);
            return user != null ? 
                Ok(user) : (IActionResult)BadRequest();
        }
    }
}