using MvvmCross;
using MvvmCross.ViewModels;
using System.Net.Http;
using Together.Core.Services.HttpRequest;
using Together.Core.Services.Identity;
using Together.Core.ViewModels.Home;

namespace Together.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterServices();

            RegisterCustomAppStart<AppStart>();
        }

        private void RegisterServices()
        {
            Mvx.IoCProvider.RegisterType(() => new HttpClient());
            Mvx.IoCProvider.RegisterType<IRequestProvider, RequestProvider>();
            Mvx.IoCProvider.RegisterType<IAuthenticationService, AuthenticationService>();
        }
    }
}
