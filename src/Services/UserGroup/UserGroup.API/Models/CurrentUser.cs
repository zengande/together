using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.UserGroup.API.Models
{
    public class CurrentUser
    {
        public int UserId { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public int? Sex { get; set; }
    }
}
