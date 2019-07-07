using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using Together.Core.Data;
using Together.Core.Services.Identity;
using Together.Core.ViewModels.Account;
using Together.Core.ViewModels.Home;

namespace Together.Core
{
    public class AppStart : MvxAppStart
    {
        private readonly IAuthenticationService _authentication;
        private readonly DbContext _dbContext;
        public AppStart(IMvxApplication application, IMvxNavigationService navigationService,
            IAuthenticationService authentication,
            DbContext dbContext)
            : base(application, navigationService)
        {
            _authentication = authentication;
            _dbContext = dbContext;
        }

        protected override async Task NavigateToFirstViewModel(object hint = null)
        {
            //var startup = await _dbContext.GetStartUpAsync();
            //if (startup != null && !startup.IsFirst)
            //{
                var isAuthenticated = await _authentication.IsAuthenticated();
                if (isAuthenticated)
                {
                    await NavigationService.Navigate<HomeViewModel>();
                }
                else
                {
                    await NavigationService.Navigate<LoginViewModel>();
                }
            //}
            //else
            //{

            //}
        }
    }
}
