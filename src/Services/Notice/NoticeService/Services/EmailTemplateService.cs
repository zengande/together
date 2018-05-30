using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public class EmailTemplateService
        : IEmailTemplateService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmailTemplateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public EmailTemplate GetTemplate(int id)
        {
            // TODO : 使用缓存
            return _dbContext.EmailTemplates.SingleOrDefault(t => t.Id == id);
        }
    }
}
