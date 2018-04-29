using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class ActivitySummaryViewModel
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int NumberOfParticipants { get; set; }
        public int? LimitsNum { get; set; }
        public string Address { get; set; }
    }
}
