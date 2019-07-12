using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Windows.Input;

namespace Together.Core.ViewModels
{
    public abstract class BaseViewModel : MvxViewModel
    {
        private bool isBusy = false;
        public bool IsBusy { get => isBusy; set => SetProperty(ref isBusy, value); }

        private string title;
        public string Title { get => title; set => SetProperty(ref title, value); }

        public DateTime? lastBackKeyDownTime;

        public bool BackKeyPressed()
        {
            if (!lastBackKeyDownTime.HasValue || DateTime.Now - lastBackKeyDownTime.Value > new TimeSpan(0, 0, 2))
            {
                lastBackKeyDownTime = DateTime.Now;
                // todo : handle back
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
