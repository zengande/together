using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Models
{
    public class InputUserViewModel
    {
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Sex { get; set; }

        public User ToEntity() => new User
        {
            Avatar = Avatar,
            Birthday = Birthday,
            Nickname = Nickname,
            Sex = Sex
        };
    }
}
