using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Nutshell.Common.Cache;
using Polly;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Together.Notice.Models;

namespace Together.Notice.Services
{
    public class EmailSender
        : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailSender> _logger;
        private readonly IRedisCacheService _redis;
        public EmailSender(IOptions<EmailSettings> options,
            ILogger<EmailSender> logger,
            IRedisCacheService redis)
        {
            _settings = options.Value;
            _client = new SendGridClient(_settings.ApiKey);
            _logger = logger;
            _redis = redis;
        }

        public async Task<bool> Send(string to, string subject, string plainTextContent, string htmlContent)
        {
            var from = new EmailAddress(_settings.From, _settings.Name);
            var recipient = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(from, recipient, subject, plainTextContent, htmlContent);
            var response = await _client.SendEmailAsync(message);
            return response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }

        /// <summary>
        ///     发送邮件
        /// </summary>
        /// <param name="mailTo">收件人
        /// <param name="subject">主题
        /// <param name="body">内容
        /// <returns></returns>
        public async Task<bool> Send(string mailTo, string subject, string body)
        {
            return await Send(new[] { mailTo }, null, subject, body, true, null);
        }

        /// <summary>
        ///     发送邮件
        /// </summary>
        /// <param name="mailTo">收件人
        /// <param name="subject">主题
        /// <param name="body">内容
        /// <returns></returns>
        public async Task<bool> Send(string[] mailTo, string subject, string body)
        {
            return await Send(mailTo, null, subject, body, true, null);
        }

        /// <summary>
        ///     发送邮件
        /// </summary>
        /// <param name="mailTo">收件人
        /// <param name="subject">主题
        /// <param name="body">内容
        /// <param name="attachmentsPath">附件
        /// <returns></returns>
        public async Task<bool> Send(string[] mailTo, string subject, string body, string[] attachmentsPath)
        {
            return await Send(mailTo, null, subject, body, true, attachmentsPath);
        }

        /// <summary>
        ///     发送邮件
        /// </summary>
        /// <param name="mailTo">收件人
        /// <param name="mailCcArray">抄送
        /// <param name="subject">主题
        /// <param name="body">内容
        /// <param name="isBodyHtml">是否Html
        /// <param name="attachmentsPath">附件
        /// <returns></returns>
        public async Task<bool> Send(string[] mailTo, string[] mailCcArray, string subject, string body, bool isBodyHtml, string[] attachmentsPath)
        {
            if (mailTo == null)
            {
                throw new ArgumentNullException(nameof(mailTo));
            }
            var recordset = new List<NoticeRecord>();

            foreach (var to in mailTo)
            {
                recordset.Add(new NoticeRecord
                {
                    To = to,
                    SendingTime = DateTime.Now,
                    Status = (int)NotificationStatus.Success
                });
            }

            try
            {
                await SendEmailAsync(mailTo, mailCcArray, subject, body, isBodyHtml, attachmentsPath);
            }
            catch (Exception e)
            {
                recordset.ForEach(r =>
                {
                    r.Status = (int)NotificationStatus.Failure;
                    r.Remarks = $"发送失败：{e}";
                });
            }

            await _redis.ListLeftPushAsync(Constants.NoticeRecordRedisKey, JsonConvert.SerializeObject(recordset));
            return true;
        }

        private async Task SendEmailAsync(string[] mailTo, string[] mailCcArray, string subject, string body, bool isBodyHtml, string[] attachmentsPath)
        {
            var retry = Policy.Handle<SmtpException>()
                            .WaitAndRetryAsync(new TimeSpan[]
                            {
                                 TimeSpan.FromSeconds(1),
                                 TimeSpan.FromSeconds(3),
                                 TimeSpan.FromSeconds(5)
                            }, (exception, timespan, count, context) =>
                            {
                                _logger.LogError($"发送邮件失败，{timespan.TotalSeconds}秒后第{count}次重试！");
                                _logger.LogError($"错误日志：{exception}");

                            });
            await retry.ExecuteAsync(async () =>
            {
                //使用指定的邮件地址初始化MailAddress实例
                var @from = new MailAddress(_settings.From);
                //初始化MailMessage实例
                var message = new MailMessage();
                //向收件人地址集合添加邮件地址
                if (mailTo != null)
                {
                    foreach (string t in mailTo)
                    {
                        message.To.Add(t);

                    }
                }

                //向抄送收件人地址集合添加邮件地址
                if (mailCcArray != null)
                {
                    foreach (string t in mailCcArray)
                    {
                        message.CC.Add(t);
                    }
                }
                //发件人地址
                message.From = @from;
                //电子邮件的标题
                message.Subject = subject;
                //电子邮件的主题内容使用的编码
                message.SubjectEncoding = Encoding.UTF8;

                //电子邮件正文
                message.Body = body;

                //电子邮件正文的编码
                message.BodyEncoding = Encoding.Default;

                message.Priority = MailPriority.Normal;

                message.IsBodyHtml = isBodyHtml;

                //在有附件的情况下添加附件
                if (attachmentsPath != null && attachmentsPath.Length > 0)
                {
                    foreach (string path in attachmentsPath)
                    {
                        var attachFile = new System.Net.Mail.Attachment(path);
                        message.Attachments.Add(attachFile);
                    }
                }
                using (var smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
                    smtp.Host = _settings.StmpHost;
                    smtp.Port = _settings.StmpPort;
                    smtp.EnableSsl = true;
                    smtp.Timeout = 10000;

                    //将邮件发送到SMTP邮件服务器
                    await smtp.SendMailAsync(message);
                }
            });
        }
    }
}

