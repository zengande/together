using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Together.BuildingBlocks.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private IHttpContextAccessor _context;
        public IdentityService(IHttpContextAccessor context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string GetUserIdentity()
        {
            return _context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public UserInfo GetUserInfo()
        {
            var userInfo = new UserInfo
            {
                Id = GetUserIdentity(),
                Nickname = GetUserName(),
            };

            return userInfo;
        }

        public string GetUserName()
        {
            return _context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "name")?.Value;
        }
    }
}
