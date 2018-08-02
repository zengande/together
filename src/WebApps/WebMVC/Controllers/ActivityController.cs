using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models.Activities;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class ActivityController
        : Controller
    {
        private readonly ICategoriesService _categoriesService;

        public ActivityController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //var categories = await _categoriesService.GetCategories();
            //ViewData["ParentCategories"] = categories;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ActivityCreateViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{
            //    return View(viewModel);
            //}
            return View(viewModel);
        }
    }
}
