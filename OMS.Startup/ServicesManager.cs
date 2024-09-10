using Microsoft.Extensions.DependencyInjection;
using System;

namespace OMS.Startup
{
    public static class ServicesManager
    {
        private static IServiceProvider _serviceProvider;

        public static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ICacheManager, CacheManager>();
            services.AddScoped<ICacheService, CacheService>();
            return services.BuildServiceProvider();
        }

        public static IServiceProvider GetServiceProvider()
        {
            return _serviceProvider?;
        }
    }
}
