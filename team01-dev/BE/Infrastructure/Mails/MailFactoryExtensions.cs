using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Mails
{
    public static class MailFactoryExtensions
    {
        public static IServiceCollection InitMail(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IEmailConfiguration, EmailConfiguration>();
            return services;
        }

        public static IServiceCollection AddEmail(this IServiceCollection services, Action<EmailConfiguration> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            services.Configure(configure);
            services.InitMail();
            return services;
        }
    }
}
