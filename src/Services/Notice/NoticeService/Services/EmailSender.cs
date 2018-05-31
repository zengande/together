using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Together.Notice.Services
{
    public class EmailSender
        : IEmailSender
    {
        private readonly SendGridClient _client;
        private readonly EmailSettings _settings;
        public EmailSender(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
            _client = new SendGridClient(_settings.ApiKey);
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
            return await Task.Factory.StartNew(() =>
            {
                try
                {
                    var @from = new MailAddress(_settings.From); //使用指定的邮件地址初始化MailAddress实例
                    var message = new MailMessage(); //初始化MailMessage实例
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
                    try
                    {
                        var smtp = new SmtpClient
                        {
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
                            Host = _settings.StmpHost,
                            Port = _settings.StmpPort,
                            EnableSsl = false
                        };

                        //将邮件发送到SMTP邮件服务器
                        smtp.Send(message);

                        //todo:记录日志
                        return true;
                    }

                    catch (SmtpException ex)
                    {
                        //todo:记录日志
                        throw ex;
                    }

                }

                catch (SmtpException ex)
                {
                    //todo:记录日志
                    throw ex;
                }
            });
        }
    }
}

