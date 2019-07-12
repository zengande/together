using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Together.Core.Services.Notification;

namespace Together.Core.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IToastService _toast;
        public LoginViewModel(IToastService toast)
        {
            _toast = toast;
        }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                if (value != username)
                {
                    username = value;
                    SetProperty(ref username, value);
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (value != password)
                {
                    password = value;
                    SetProperty(ref password, value);
                }
            }
        }


        private MvxCommand loginCommand;
        public ICommand LoginCommand => loginCommand ?? new MvxCommand(Login);


        private void Login()
        {
            if (username == "admin" && password == "admin")
            {
                _toast.Alert("login successful!");
            }
            else
            {
                _toast.Alert("login failed!");
            }
        }
    }
}
