using MvvmCross.Forms.Platforms.Uap.Views;
using Windows.UI.Xaml;
using Xamarin.Forms;

namespace Together.UWP
{
    sealed partial class App
    {
        public App()
        {
            InitializeComponent();
        }
    }

    public abstract class MvxFormsApp : MvxWindowsApplication<Setup, Core.App, UI.App, MainPage>
    {
    }
}
