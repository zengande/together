using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Together.UserGroup.API.Infrastructure.Models;

namespace Together.UserGroup.API.Models
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Email { get; set; }

        public User ToEntity() => new User
        {
            Id = Id,
            Nickname = Nickname,
            Email = Email
        };
    }
}
