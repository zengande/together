using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Models;

namespace Together.UserGroup.API.Controllers
{
    public class BaseController
        : ControllerBase
    {
        protected CurrentUser CurrentUser => new CurrentUser
        {
            UserId = 1
        };
    }
}
