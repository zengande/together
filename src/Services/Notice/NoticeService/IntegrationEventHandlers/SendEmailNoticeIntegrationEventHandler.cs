﻿using DotNetCore.CAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Together.Notice.IntegrationEvents;
using Together.Notice.Services;

namespace Together.Notice.IntegrationEventHandlers
{
    public class SendEmailNoticeIntegrationEventHandler
        : ICapSubscribe
    {
        private readonly IEmailSender _sender;
        public SendEmailNoticeIntegrationEventHandler(IEmailSender sender)
        {
            _sender = sender;
        }

        [CapSubscribe("Together.Notice.Email")]
        public async Task SendEmailNotice(SendEmailNoticeIntegrationEvent @event)
        {
            // TODO : 将记录添加到数据库
            var result = await _sender.Send(@event.To, @event.Subject, CleanHtml(@event.HtmlContent), @event.HtmlContent);
        }

        /// <summary>
        /// 去掉HTML中的所有标签,只留下纯文本
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        private string CleanHtml(string strHtml)
        {
            if (string.IsNullOrEmpty(strHtml)) return strHtml;
            //删除脚本
            //Regex.Replace(strHtml, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase)
            strHtml = Regex.Replace(strHtml, @"(\<script(.+?)\</script\>)|(\<style(.+?)\</style\>)", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //删除标签
            var r = new Regex(@"</?[^>]*>", RegexOptions.IgnoreCase);
            Match m;
            for (m = r.Match(strHtml); m.Success; m = m.NextMatch())
            {
                strHtml = strHtml.Replace(m.Groups[0].ToString(), "");
            }
            return strHtml.Trim();
        }
    }
}
