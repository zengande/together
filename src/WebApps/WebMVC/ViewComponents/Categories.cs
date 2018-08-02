using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly ICategoriesService _categoriesService;

        public Categories(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CategorySelect vm)
        {
            var categories = await _categoriesService.GetCategories(vm?.ParentId);
            vm.Categories = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.Text,
                Value = c.Id.ToString()
            });
            return View(vm);
        }
    }
}
