using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Together.Extensions.Claims;

namespace Together.Activity.API.Controllers
{
    public class BaseController
        : ControllerBase
    {
        protected CurrentUserInfo CurrentUser
        {
            get => HttpContext.User.GetCurrentUserInfo();
        }
    }
}
