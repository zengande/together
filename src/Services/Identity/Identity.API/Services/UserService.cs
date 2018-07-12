using DnsClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Nutshell.Resilience.HttpRequest.abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Identity.API.Configurations;
using Together.Identity.API.Models;

namespace Together.Identity.API.Services
{
    public class UserService
        : IUserService
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> FindByEmail(string user)
        {
            return await _userManager.FindByEmailAsync(user);
        }

        public async Task<bool> ValidateCredentials(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task SignIn(ApplicationUser user, AuthenticationProperties props)
        {
            await _signInManager.SignInAsync(user, props);
        }
    }
}
