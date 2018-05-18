using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Models
{
    public class CurrentUser
    {
        public string Avatar { get; set; }
        public string UserId { get; set; }
        public int Sex { get; set; }
        public string Nickname { get; set; }

    }
}
