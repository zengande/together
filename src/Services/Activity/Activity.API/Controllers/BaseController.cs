using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Activity.API.Models;

namespace Together.Activity.API.Controllers
{
    public class BaseController
        : ControllerBase
    {
        protected CurrentUser CurrentUser
        {
            get
            {
                var id = new Random().Next(1000, 9999999);
                return new CurrentUser
                {
                    UserId = "1545456",//id,
                    Nickname = $"测试用户{id}",
                    Avatar = "http://c.hiphotos.baidu.com/image/h%3D300/sign=9be5ad4e6f2762d09f3ea2bf90ec0849/5243fbf2b21193138277eddd69380cd791238da2.jpg"
                };
            }
        }
    }
}
