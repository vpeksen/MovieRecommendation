using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.MailSender
{
    public interface IMailSender
    {
        IResult SendMail(string from, string to, string subject, string message);
    }
}
