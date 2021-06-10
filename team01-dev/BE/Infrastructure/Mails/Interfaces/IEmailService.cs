using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Mails
{
    public interface IEmailService
    {
        void SendMultiple(EmailMessageMultiple emailMessage);
        void Send(EmailMessage emailMessage);
        Task<List<EmailMessageMultiple>> ReceiveEmail(int maxCount = 10);
        Task<bool> SendMail(EmailMessageMultiple emailMessage);
    }
}
