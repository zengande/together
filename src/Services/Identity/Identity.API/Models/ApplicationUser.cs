using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Together.Identity.API.Models
{
    public class ApplicationUser
        : IdentityUser
    {
        public string Nickname { get; set; }
        public Gender Gender { get; set; }
        public string Avatar { get; set; }
    }
}
