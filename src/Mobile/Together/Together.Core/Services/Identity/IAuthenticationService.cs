using System.Threading.Tasks;
using Together.Core.Models.Identity;

namespace Together.Core.Services.Identity
{
    public interface IAuthenticationService
    {
        string CreateAuthorizationRequest();
        string CreateLogoutRequest(string token);
        Task<UserToken> GetTokenAsync(string username, string password);
        Task<bool> IsAuthenticated();
    }
}
