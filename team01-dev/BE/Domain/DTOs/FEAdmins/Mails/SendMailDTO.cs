using Infrastructure.Mails;

namespace Domain.DTOs.Mails
{
    public class SendMailDTO
    {
        public EmailAddress ToAddresse { get; set; }
        public EmailAddress FromAddresse { get; set; }
        public AttachmentFile AttachmentFile { get; set; }
        public string KeyTemplate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailContentTypeEnum ContentType { get; set; }
    }
}
