using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models.Organization;

namespace WebMVC.Controllers
{
    public class OrganizationController : Controller
    {
        [HttpGet]

        public IActionResult Create()
        {
            // 获取用户所在位置
            ViewData["Location"] = "杭州市，浙江省，中国";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                return View();
            }
            return View(model);
        }
    }
}