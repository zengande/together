using System;
using System.Collections.Generic;
using System.Text;
using Together.Core.Models;

namespace Together.Core.Services.Notification
{
    public interface IToastService
    {
        void Alert(string message, ToastLength time = ToastLength.Short);
    }
}
