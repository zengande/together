using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Activity.API.Applications.Models
{
    public class CurrentUser
    {
        public string Avatar { get; set; }
        public int UserId { get; set; }

        public string Nickname { get; set; }

    }
}
