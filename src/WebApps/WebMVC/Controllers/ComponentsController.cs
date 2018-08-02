using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class ComponentsController
        : Controller
    {
        public IActionResult Categories(CategorySelect vm)
        {
            return ViewComponent("Categories", vm);
        }
    }
}
