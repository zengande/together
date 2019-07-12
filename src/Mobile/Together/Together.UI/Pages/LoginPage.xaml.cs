using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Together.Core.ViewModels.Account;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Together.UI.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
		public LoginPage ()
		{
			InitializeComponent ();
		}
	}
}