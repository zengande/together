using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Infrastructure.API;
using WebMVC.ViewModels;

namespace WebMVC.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly ICategoryAPI _categoryAPI;

        public Categories(ICategoryAPI categoryAPI)
        {
            _categoryAPI = categoryAPI;
        }

        public async Task<IViewComponentResult> InvokeAsync(CategorySelect vm)
        {
            try
            {
                var categories = await _categoryAPI.GetCategories(vm?.ParentId);
                vm.Categories = categories?.Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = c.Text,
                    Value = c.Id.ToString()
                });
                return View(vm);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
