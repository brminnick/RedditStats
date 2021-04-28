using System;
using Microsoft.Extensions.DependencyInjection;
using RedditStats.Common;

namespace RedditStats.Console
{
    static class ServiceCollection
    {
        static Lazy<IServiceProvider> _serviceProviderHolder = new(CreateContainer);

        public static IServiceProvider ServiceProvider => _serviceProviderHolder.Value;

        static IServiceProvider CreateContainer()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

            // Services
            services.AddSingleton<RedditApiService>();

            // Refit Clients
            services.AddSingleton(Refit.RestService.For<IRedditApi>(RedditApiConstants.BaseUrl));

            return services.BuildServiceProvider();
        }
    }
}
