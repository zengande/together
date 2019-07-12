using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Together.Extensions.Claims;
using Together.Mvc.Core.Models;

namespace Together.Mvc.Core
{
    public abstract class ApiController : ControllerBase
    {
        protected ActionResult<JsonResultModel<T>> OkResponse<T>(T data)
        {
            return Ok(new JsonResultModel<object>(true, data));
        }

        protected ActionResult<JsonResultModel<bool>> BadResponse(IEnumerable<string> errors, string code)
        {
            return BadRequest(new JsonResultModel<bool>(errors, code));
        }

        protected CurrentUserInfo CurrentUser => HttpContext.User.GetCurrentUserInfo(); 
    }
}
