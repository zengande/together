using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.ViewModels
{
    public class DashboardViewModel
    {
        public long Total { get; set; }
        public long Success { get; set; }
        public long Failure { get; set; }
        public List<string> Category { get; set; }
        public List<dynamic> Series { get; set; }
    }
}
