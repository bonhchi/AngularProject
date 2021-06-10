using System.Collections.Generic;

namespace Infrastructure.Mails
{
    public class EmailMessageMultiple
    {
        public EmailMessageMultiple()
        {
            ToAddresses = new List<EmailAddress>();
            FromAddresses = new List<EmailAddress>();
            ReplyAddresses = new List<EmailAddress>();
            AttachmentFiles = new List<AttachmentFile>();
        }

        public List<EmailAddress> ToAddresses { get; set; }
        public List<EmailAddress> FromAddresses { get; set; }
        public List<EmailAddress> ReplyAddresses { get; set; }
        public List<EmailAddress> CcAddresses { get; set; }
        public List<AttachmentFile> AttachmentFiles { get; set; }

        public string KeyTemplate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailContentTypeEnum ContentType { get; set; }
    }


    public class EmailMessage
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
    public class AttachmentFile
    {
        public string FileName { get; set; }
        public byte[] File { get; set; }
    }
}
