using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public static class DataDiConfig
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            //transient
            ////DbContext
            services.AddDbContext<ShopDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ShopDbContext"), b => b.MigrationsAssembly("Data")).ConfigureWarnings(c => c.Log((RelationalEventId.CommandExecuting, LogLevel.Debug)));
            }, ServiceLifetime.Transient);
            services.AddScoped<IDataContextAsync, ShopDbContext>();


        }
    }
}
