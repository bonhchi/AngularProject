using Infrastructure.Mails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.AppConfig
{
    public static class GmailConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            MailFactoryExtensions.AddEmail(services, configuration.GetSection("GmailConfig").Bind);
        }
    }
}
