using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.Dtos
{
    public class ParticipantDto
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int Sex { get; set; }
        public DateTime JoinTime { get; set; }
        public bool IsOwner { get; set; }
    }
}
