using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Together.Core.Services.Identity;
using Together.Core.ViewModels.Account;
using Together.Core.ViewModels.Home;

namespace Together.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IAuthenticationService _authentication;
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService, IAuthenticationService authentication)
            : base(application, navigationService)
        {
            _authentication = authentication;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            var isAuthenticated = await _authentication.IsAuthenticated();
            if (isAuthenticated)
            {
                await NavigationService.Navigate<HomeViewModel>();
            }
            else
            {
                await NavigationService.Navigate<LoginViewModel>();
            }
        }
    }
}
