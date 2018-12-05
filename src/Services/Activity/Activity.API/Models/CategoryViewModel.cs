using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Key { get; set; }
        public int? ParentId { get; set; }
        public int Sort { get; set; }
    }
}
