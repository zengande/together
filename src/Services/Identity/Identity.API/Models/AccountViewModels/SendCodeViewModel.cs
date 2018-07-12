using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Identity.API.Models.AccountViewModels
{
    public class SendCodeViewModel
    {
        public object Providers { get; internal set; }
        public string ReturnUrl { get; internal set; }
        public bool RememberMe { get; internal set; }
    }
}
