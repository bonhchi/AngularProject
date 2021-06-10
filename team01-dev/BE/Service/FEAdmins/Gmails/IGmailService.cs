using Common.Http;
using Domain.DTOs.Mails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Gmails
{
    public interface IGmailService
    {
        ReturnMessage<bool> SendMail(SendMailDTO dto);
        ReturnMessage<bool> ReplyMail(ReplyEmailDTO dto);
    }
}
