using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class ActivityViewModel
    {
        public ActivityViewModel()
        {
            Participants = new List<ParticipantViewModel>();
        }

        public int ActivityId { get; set; }
        public int OwnerId { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ActivityTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Address { get; set; }
        public int? LimitsNum { get; set; }
        public string Status { get; set; }

        public List<ParticipantViewModel> Participants { get; set; }
    }

    public class ParticipantViewModel
    {
        public string Nickname { get; set; }
        public DateTime JoinTime { get; set; }
        public string Avatar { get; set; }
        public int Sex { get; set; }
    }
}
