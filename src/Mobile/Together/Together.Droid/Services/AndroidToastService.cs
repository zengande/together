using Android.App;
using Android.Widget;
using System;
using Together.Core.Services.Notification;
using Together.Droid.Services;

namespace Together.Droid.Services
{
    public class AndroidToastService : IToastService
    {
        public void Alert(string message, Core.Models.ToastLength time = Core.Models.ToastLength.Short)
        {
            if (time == Core.Models.ToastLength.Short)
            {
                Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
            }
        }
    }
}