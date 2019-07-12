using MvvmCross;
using MvvmCross.Forms.Platforms.Uap.Core;
using MvvmCross.IoC;
using Together.Core.Services.Notification;
using Together.UWP.Services;

namespace Together.UWP
{
    public class Setup : MvxFormsWindowsSetup<Core.App, UI.App>
    {
        protected override IMvxIoCProvider CreateIocProvider()
        {
            var provider = base.CreateIocProvider();

            provider.RegisterType<IToastService, UWPToastService>();

            return provider;
        }
    }
}
