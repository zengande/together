using Android.App;
using MvvmCross;
using MvvmCross.Forms.Platforms.Android.Core;
using MvvmCross.IoC;
using Together.Core.Services.Notification;
using Together.Droid.Services;

#if DEBUG
[assembly: Application(Debuggable = true)]
#else
[assembly: Application(Debuggable = false)]
#endif

namespace Together.Droid
{
    public class Setup : MvxFormsAndroidSetup<Core.App, UI.App>
    {
        protected override IMvxIoCProvider CreateIocProvider()
        {
            var provider = base.CreateIocProvider();

            provider.RegisterType<IToastService, AndroidToastService>();

            return provider;
        }
    }
}
