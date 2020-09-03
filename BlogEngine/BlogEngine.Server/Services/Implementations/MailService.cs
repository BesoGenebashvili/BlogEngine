using System;
using System.Linq;
using BlogEngine.Server.Services.Abstractions;
using BlogEngine.Shared.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BlogEngine.Server.Services.Implementations
{
    public class MailService : IMailService
    {
        private readonly SMTPConfig _SMTPConfig;

        public MailService(IOptions<SMTPConfig> options)
        {
            _SMTPConfig = options.Value;
        }

        public async Task<bool> SendAsync(MailModel mailModel)
        {
            if (mailModel == null)
            {
                throw new ArgumentNullException(nameof(mailModel));
            }

            return await Task.Run(() => Send(mailModel));
        }

        private bool Send(MailModel mailModel)
        {
            try
            {
                var fromMailAdress = new MailAddress(_SMTPConfig.SMTPFrom, _SMTPConfig.DisplayName);
                var toMailAddress = new MailAddress(mailModel.EmailAddress, mailModel.DisplayName);

                SmtpClient smtpClient = new SmtpClient
                {
                    Host = _SMTPConfig.Host,
                    Port = _SMTPConfig.Port,
                    EnableSsl = _SMTPConfig.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_SMTPConfig.SMTPFrom, _SMTPConfig.Password)
                };

                using (MailMessage mailMessage = new MailMessage(fromMailAdress, toMailAddress)
                {
                    IsBodyHtml = mailModel.IsBodyHtml,
                    Subject = mailModel.Subject,
                    Body = mailModel.Body
                })
                {
                    var bccAdresses = _SMTPConfig
                        .BCCRecipientList.Split(",")
                        .Select(e => new MailAddress(e.Trim()))
                        .ToList();

                    bccAdresses.ForEach(bcc => mailMessage.Bcc.Add(bcc));

                    smtpClient.Send(mailMessage);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}