using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class BaseController
        : Controller
    {
        protected async Task<string> GetUserTokenAsync()
        {
            return await HttpContext.GetTokenAsync("access_token");
        }
    }
}
