using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.ViewModels.Activity;

namespace WebMVC.ViewModels.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            ActivitySummaries = new List<ActivitySummaryViewModel>();
            ActivityCategories = new List<ActivityCategoryViewModel>();
        }
        public IEnumerable<ActivitySummaryViewModel> ActivitySummaries { get; set; }
        public IEnumerable<ActivityCategoryViewModel> ActivityCategories { get; set; }
    }
}
