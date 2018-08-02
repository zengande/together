using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class CategorySelect
    {
        public string Id { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public int? SelectedCategory { get; set; }
        public int? ParentId { get; set; }
    }
}
