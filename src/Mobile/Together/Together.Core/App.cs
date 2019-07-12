using MvvmCross;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Net.Http;
using Together.Core.Data;
using Together.Core.Services.HttpRequest;
using Together.Core.Services.Identity;
using Together.Core.Services.Theme;
using Together.Core.ViewModels.Home;
using Xamarin.Forms;

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
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "together.db");
            Mvx.IoCProvider.RegisterSingleton(new DbContext(dbPath));

            Mvx.IoCProvider.RegisterType(() => new HttpClient());
            Mvx.IoCProvider.RegisterType<IRequestProvider, RequestProvider>();
            Mvx.IoCProvider.RegisterType<IAuthenticationService, AuthenticationService>();
            Mvx.IoCProvider.RegisterType<IPalette, Palette>();
        }
    }
}
