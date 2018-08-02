using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class Category
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Text { get; set; }
        public int? ParentId { get; set; }
        public int Sort { get; set; }
    }
}
