using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.ViewModels.Activity
{
    public class ActivitySummaryViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OwnerId { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int? LimitsNum { get; set; }
    }
}
