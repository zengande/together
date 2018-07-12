using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Identity.API.Models;

namespace Together.Identity.API.Services
{
    public interface IUserService
    {
        Task<bool> ValidateCredentials(ApplicationUser user, string password);
        Task<ApplicationUser> FindByEmail(string user);
        Task SignIn(ApplicationUser user, AuthenticationProperties props);
    }
}
