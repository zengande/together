using IdentityModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Together.Extensions.Claims
{
    public static class ClaimsPrincipalExtensions
    {
        public static CurrentUserInfo GetCurrentUserInfo(this ClaimsPrincipal user)
        {
            if (user.Identity is ClaimsIdentity identity)
            {
                return GetUserFromClaims(identity.Claims);
            }
            return null;
        }

        private static CurrentUserInfo GetUserFromClaims(IEnumerable<Claim> claims)
        {
            if (claims?.Count() <= 0)
            {
                return null;
            }
            var user = new CurrentUserInfo
            {
                UserId = claims.GetValue(JwtClaimTypes.Subject),
                UserName = claims.GetValue(JwtClaimTypes.PreferredUserName),
                Email = claims.GetValue(JwtClaimTypes.Email),
                Nickname = claims.GetValue(nameof(CurrentUserInfo.Nickname)),
                Avatar = claims.GetValue(nameof(CurrentUserInfo.Avatar)),
            };

            return user;
        }
    }

    public static class ClaimsExtensions
    {
        public static string GetValue(this IEnumerable<Claim> claims, string type, string defValue = "")
        {
            return claims.FirstOrDefault(c => c.Type == type)?.Value ?? defValue;
        }
    }
}
