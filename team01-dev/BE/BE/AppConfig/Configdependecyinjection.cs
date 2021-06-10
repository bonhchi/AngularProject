using AutoMapper;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BE.AppConfig
{
    public static class Configdependecyinjection
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            //singleton
            ////Mapper

            ////Find all auto mapper profile
            //services.AddAutoMapper(typeof(Startup));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            ////Find and start only
            var mapperConfig = new AutoMapper.MapperConfiguration(t =>
            {
                t.AddProfile(new AutoMapperProfile());
                // t.AddProfile(new AutoMapperServiceProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

        }
    }
}
