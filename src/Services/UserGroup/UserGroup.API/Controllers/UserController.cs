using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Together.UserGroup.API.Infrastructure.Services;

namespace UserGroup.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("{userId:int}")]
        [HttpGet]
        public async Task<IActionResult> Get(int userId)
        {
            var user = await _userService.GetAsync(userId);
            return Ok(user);
        }
    }
}