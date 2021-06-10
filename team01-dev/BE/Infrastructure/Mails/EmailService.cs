using Infrastructure.Extensions;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Mails
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailConfiguration> emailConfiguration, ILogger<EmailService> logger)
        {
            _emailConfiguration = emailConfiguration.Value;
            _logger = logger;
        }

        public async Task<List<EmailMessageMultiple>> ReceiveEmail(int maxCount = 10)
        {

            using (var emailClient = new Pop3Client())
            {
                await emailClient.ConnectAsync(_emailConfiguration.PopServer, _emailConfiguration.PopPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                await emailClient.AuthenticateAsync(_emailConfiguration.PopUsername, _emailConfiguration.PopPassword);

                List<EmailMessageMultiple> emails = new List<EmailMessageMultiple>();
                for (int i = 0; i < emailClient.Count && i < maxCount; i++)
                {
                    var message = await emailClient.GetMessageAsync(i);
                    var emailMessage = new EmailMessageMultiple
                    {
                        Content = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                        Subject = message.Subject
                    };
                    emailMessage.ToAddresses.AddRange(message.To.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emailMessage.FromAddresses.AddRange(message.From.Select(x => (MailboxAddress)x).Select(x => new EmailAddress { Address = x.Address, Name = x.Name }));
                    emails.Add(emailMessage);
                }

                return emails;
            }



        }

        public void Send(EmailMessage emailMessage)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    var message = new MimeMessage();
                    message.To.Add(new MailboxAddress(emailMessage.ToAddresse.Name, emailMessage.ToAddresse.Address));
                    message.From.Add(new MailboxAddress(emailMessage.FromAddresse.Name, emailMessage.FromAddresse.Address));
                    if (emailMessage.ReplyAddresse.IsNotNullOrEmpty())
                    {
                        message.ReplyTo.Add(new MailboxAddress(emailMessage.ReplyAddresse.Name, emailMessage.ReplyAddresse.Address));
                    }
                    message.Subject = emailMessage.Subject;
                    var builder = new BodyBuilder();
                    if (emailMessage.ContentType == EmailContentTypeEnum.HTML)
                    {
                        builder.HtmlBody = emailMessage.Content;
                    }
                    else if (emailMessage.ContentType == EmailContentTypeEnum.TEXT)
                    {
                        builder.TextBody = emailMessage.Content;
                    }
                    if (emailMessage.AttachmentFile != null)
                    {
                        builder.Attachments.Add(emailMessage.AttachmentFile.FileName, emailMessage.AttachmentFile.File);
                    }
                    //We will say we are sending HTML. But there are options for plaintext etc. 
                    message.Body = builder.ToMessageBody();
                    //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                    using (var emailClient = new SmtpClient())
                    {
                        //The last parameter here is to use SSL (Which you should!)
                        emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                        //Remove any OAuth functionality as we won't be using it. 
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                        await emailClient.SendAsync(message);

                        emailClient.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
            });
        }

        public void SendMultiple(EmailMessageMultiple emailMessage)
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    var message = new MimeMessage();
                    message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                    message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                    if (emailMessage.CcAddresses.IsNotNullOrEmpty())
                    {
                        message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                    }
                    if (emailMessage.ReplyAddresses.IsNotNullOrEmpty())
                    {
                        message.ReplyTo.AddRange(emailMessage.ReplyAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                    }

                    message.Subject = emailMessage.Subject;
                    var builder = new BodyBuilder();
                    if (emailMessage.ContentType == EmailContentTypeEnum.HTML)
                    {
                        builder.HtmlBody = emailMessage.Content;
                    }
                    else if (emailMessage.ContentType == EmailContentTypeEnum.TEXT)
                    {
                        builder.TextBody = emailMessage.Content;
                    }




                    if (emailMessage.AttachmentFiles != null)
                    {
                        foreach (var file in emailMessage.AttachmentFiles)
                        {
                            builder.Attachments.Add(file.FileName, file.File);
                        }
                    }
                    //We will say we are sending HTML. But there are options for plaintext etc. 
                    message.Body = builder.ToMessageBody();
                    //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                    using (var emailClient = new SmtpClient())
                    {
                        if (_emailConfiguration.SmtpPort == 25 || _emailConfiguration.SmtpPort == 587)
                        {
                            emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                        }
                        else
                        {
                            //The last parameter here is to use SSL (Which you should!)
                            emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                            //Remove any OAuth functionality as we won't be using it. 
                            emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                        }
                        emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                        await emailClient.SendAsync(message);

                        emailClient.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }

            });
        }

        public async Task<bool> SendMail(EmailMessageMultiple emailMessage)
        {

            var result = false;
            try
            {
                var message = new MimeMessage();
                message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                if (emailMessage.CcAddresses.IsNotNullOrEmpty())
                {
                    message.Cc.AddRange(emailMessage.CcAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }
                if (emailMessage.ReplyAddresses.IsNotNullOrEmpty())
                {
                    message.ReplyTo.AddRange(emailMessage.ReplyAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
                }

                message.Subject = emailMessage.Subject;
                var builder = new BodyBuilder();
                if (emailMessage.ContentType == EmailContentTypeEnum.HTML)
                {
                    builder.HtmlBody = emailMessage.Content;
                }
                else if (emailMessage.ContentType == EmailContentTypeEnum.TEXT)
                {
                    builder.TextBody = emailMessage.Content;
                }




                if (emailMessage.AttachmentFiles != null)
                {
                    foreach (var file in emailMessage.AttachmentFiles)
                    {
                        builder.Attachments.Add(file.FileName, file.File);
                    }
                }
                //We will say we are sending HTML. But there are options for plaintext etc. 
                message.Body = builder.ToMessageBody();
                //Be careful that the SmtpClient class is the one from Mailkit not the framework!
                using (var emailClient = new SmtpClient())
                {
                    if (_emailConfiguration.SmtpPort == 25 || _emailConfiguration.SmtpPort == 587)
                    {
                        emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                    }
                    else
                    {
                        //The last parameter here is to use SSL (Which you should!)
                        emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);

                        //Remove any OAuth functionality as we won't be using it. 
                        emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                    }
                    emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);

                    await emailClient.SendAsync(message);

                    emailClient.Disconnect(true);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                _logger.LogError(ex.Message, ex);
            }

            return result;
        }


    }

}
