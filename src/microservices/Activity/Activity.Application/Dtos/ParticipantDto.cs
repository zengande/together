using System;
using System.Collections.Generic;
using System.Text;

namespace Together.Activity.Application.Dtos
{
    public class ParticipantDto
    {
        public string UserId { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public DateTime JoinTime { get; set; }
        public bool IsOwner { get; set; }
    }
}
