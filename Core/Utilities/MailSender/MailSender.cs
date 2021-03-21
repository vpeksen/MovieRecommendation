using Core.Utilities.Messages;
using Core.Utilities.Results;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Net;
using MailKit;

namespace Core.Utilities.MailSender
{
    public class MailSender : IMailSender
    {
        public IConfiguration Configuration { get; }
        private MailOptions _mailOptions;
        public MailSender(IConfiguration configuration)
        {
            Configuration = configuration;
            _mailOptions = Configuration.GetSection("MailOptions").Get<MailOptions>();
        }

        public IResult SendMail(string from, string to, string subject, string message)
        {
            using var smtp = new SmtpClient();
            try
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Admin", from));
                email.To.Add(new MailboxAddress("User", to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Plain) { Text = message };

                // send email
                smtp.Connect(_mailOptions.SmtpHost, _mailOptions.SmtpPort, SecureSocketOptions.Auto);
                smtp.Authenticate(_mailOptions.SmtpUser, _mailOptions.SmtpPass);
                smtp.Send(email);
            }
            catch (SmtpCommandException ex)
            {
                return new ErrorResult(ex.Message);
            }
            finally
            {
                smtp.Disconnect(true);
            }
            return new SuccessResult(AspectMessages.SendMail);
        }
    }
}
