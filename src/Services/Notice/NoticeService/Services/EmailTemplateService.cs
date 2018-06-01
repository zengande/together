using Nutshell.Common.Cache;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public class EmailTemplateService
        : IEmailTemplateService
    {
        private readonly ICacheService _cacheService;
        private readonly ApplicationDbContext _dbContext;
        public EmailTemplateService(ApplicationDbContext dbContext,
            ICacheService cacheService)
        {
            _cacheService = cacheService;
            _dbContext = dbContext;
        }
        public async Task<EmailTemplate> GetTemplate(int id)
        {
            var template = await _cacheService.GetAsync<EmailTemplate>(id.ToString());
            if (template == null)
            {
                template = _dbContext.EmailTemplates.SingleOrDefault(t => t.Id == id);
                await _cacheService.AddAsync(id.ToString(), template);
            }
            return template;
        }

        public async Task<EmailTemplate> GetTemplate(string key)
        {
            var template = await _cacheService.GetAsync<EmailTemplate>(key);
            if (template == null)
            {
                template = _dbContext.EmailTemplates.SingleOrDefault(t => t.KeyWord.Trim().Equals(key.Trim(), StringComparison.CurrentCultureIgnoreCase)); ;
                await _cacheService.AddAsync(key, template);
            }
            return template;
        }

        public string Build(string template, Dictionary<string,string> keyValues)
        {
            if (keyValues != null)
            {
                foreach (var item in keyValues)
                {
                    template = template.Replace($"[${item.Key}]", item.Value);
                }
            }
            return template;
        }
    }
}
