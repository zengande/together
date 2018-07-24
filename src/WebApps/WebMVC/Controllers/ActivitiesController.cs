using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Activities;

namespace WebMVC.Controllers
{
    public class ActivitiesController
        : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return View();
            }
            return View(viewModel);
        }
    }
}
