using HospitalAPP.Email.Intrefaces;
using HospitalDomain.DTOS;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace HospitalAPP.Email.WorkEmail
{
    public class EmailSettings : IEmailSettings
    {
        private readonly MailSettings _options;

        public EmailSettings(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }

        public void SendEmail(EmailDTO email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject,
            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));
            var builder = new BodyBuilder
            {
                TextBody = email.Body // Plain text body
                                      // Uncomment the next line if you have HTML content
                                      // HtmlBody = "<html><body><p>" + email.Body + "</p></body></html>"
            };

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate(_options.Email, _options.Password);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Log the exception details for further investigation
                Console.WriteLine($"Exception: {ex.Message}");
                throw; // Re-throw the exception to handle it in the calling method
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }
    }
}