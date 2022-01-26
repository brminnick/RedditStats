using System;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RedditStats.Common;
using Refit;

namespace RedditStats.Console;

static class ServiceCollection
{
	static Lazy<IServiceProvider> _serviceProviderHolder = new(CreateContainer);

	public static IServiceProvider ServiceProvider => _serviceProviderHolder.Value;

	static IServiceProvider CreateContainer()
	{
		var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();

		services.AddRedditStatsServices();

		return services.BuildServiceProvider();
	}
}