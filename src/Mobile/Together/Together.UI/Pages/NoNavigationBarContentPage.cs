using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace Together.UI.Pages
{
    public class NoNavigationBarContentPage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public NoNavigationBarContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }

    public class NoNavigationBarContentPage : MvxContentPage
    {
        public NoNavigationBarContentPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
