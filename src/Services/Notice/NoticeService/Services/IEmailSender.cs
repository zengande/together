using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Together.Notice.Services
{
    public interface IEmailSender
    {
        [Obsolete]
        Task<bool> Send(string to, string subject, string plainTextContent, string htmlContent);

        Task<bool> Send(string mailTo, string subject, string body);
        Task<bool> Send(string[] mailTo, string subject, string body);
        Task<bool> Send(string[] mailTo, string subject, string body, string[] attachmentsPath);
        Task<bool> Send(string[] mailTo, string[] mailCcArray, string subject, string body, bool isBodyHtml, string[] attachmentsPath);
    }
}
