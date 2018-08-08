using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels
{
    public class ActivityCreateViewModel
    {
        public ActivityCreateViewModel()
        {
            IsFree = true;
            Fee = 0;
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public int ParentCategory { get; set; }
        public int? ChildCategory { get; set; }
        public string Remarks { get; set; }
        public string Details { get; set; }
        public bool IsFree { get; set; }
        public decimal? Fee { get; set; }
        public string Address_Details { get; set; }

        [Required]
        public int CategoryId
        {
            get
            {
                if (ChildCategory.HasValue)
                {
                    return ChildCategory.Value;
                }
                return ParentCategory;
            }
        }
    }
}
