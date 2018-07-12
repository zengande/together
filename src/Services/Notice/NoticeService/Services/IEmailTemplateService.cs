using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplate> GetTemplate(string key);
        Task<EmailTemplate> GetTemplate(int id);
        string Build(string template, Dictionary<string, string> keyValues);
    }
}
