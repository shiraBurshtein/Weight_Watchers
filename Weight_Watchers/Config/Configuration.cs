using AutoMapper;
using Subscriber.CORE.Interface_DAL;
using Subscriber.CORE.Interface_Service;
using Subscriber.DAL;
using Subscriber.Services;

namespace Subscriber.WebApi.Config
{
    public static class ConfigureServices
    {

        public static void ConfigurationService(this IServiceCollection services)
        {

            services.AddScoped<IWeight_WatchersService, Weight_WatchersService>();
            services.AddScoped<IWeight_WatchersRepository, Weight_WatchersRepository>();


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new WeightWatchersProfiler());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
