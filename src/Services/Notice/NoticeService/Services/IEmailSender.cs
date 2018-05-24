using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.Services
{
    public interface IEmailSender
    {
        Task<bool> Send(string to, string subject, string plainTextContent, string htmlContent);
    }
}
