using Infrastructure.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE.AppConfig
{
    public class FileCloudConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            var fileConfig = configuration.GetSection("CloudImgConfig").Get<FileConfig>();
            services.AddSingleton(fileConfig);
        }
    }
}
