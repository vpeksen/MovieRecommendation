using Business.Abstract;
using Core.Utilities.MailSender;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MailManager : IMailService
    {
        private IMailSender _mailsender;
        public MailManager(IMailSender mailsender)
        {
            _mailsender = mailsender;
        }
        public IResult SendMail(string movieName, string to)
        {
            return _mailsender.SendMail("projemailtest26@gmail.com", to, "Film Tavsiyesi", "Bu filmi izlemelisin:" + movieName);
        }
    }
}
