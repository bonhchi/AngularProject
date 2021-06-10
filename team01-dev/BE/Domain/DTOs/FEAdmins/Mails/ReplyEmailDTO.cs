using Infrastructure.Mails;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs.Mails
{
    public class ReplyEmailDTO
    {
        public EmailAddress ToAddresse { get; set; }
        public EmailAddress FromAddresse { get; set; }
        public EmailAddress ReplyAddresse { get; set; }
        public AttachmentFile AttachmentFile { get; set; }
        public string KeyTemplate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailContentTypeEnum ContentType { get; set; }
    }
}
