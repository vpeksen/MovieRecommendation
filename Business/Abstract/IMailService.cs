using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMailService
    {
        IResult SendMail(string movieName, string to);
    }
}
