using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.MailSender
{
    public class MailOptions
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPass { get; set; }
        public bool EnableSSL { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
