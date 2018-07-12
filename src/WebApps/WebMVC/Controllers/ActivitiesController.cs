using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Controllers
{
    public class ActivitiesController
        : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
