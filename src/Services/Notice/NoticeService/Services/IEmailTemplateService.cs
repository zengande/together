using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public interface IEmailTemplateService
    {
        EmailTemplate GetTemplate(int id);
    }
}
