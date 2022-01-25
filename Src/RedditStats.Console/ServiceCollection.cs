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

		// Services
		services.AddSingleton<AdvocateService>();
		services.AddSingleton<RedditApiService>();

		// Refit Clients
		services.AddRefitClient<IRedditApi>()
				.ConfigureHttpClient(client =>
				{
					client.BaseAddress = new Uri(RedditApiConstants.BaseRedditApiUrl);
					client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue(nameof(RedditStats))));
				})
				.ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
				.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

		services.AddRefitClient<IAdvocateApi>()
				.ConfigureHttpClient(client =>
				{
					client.BaseAddress = new Uri(AdvocateConstants.BaseAdvocateApi);
					client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue(nameof(RedditStats))));
				})
				.ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
				.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

		return services.BuildServiceProvider();

		static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
		static DecompressionMethods getDecompressionMethods() => DecompressionMethods.Deflate | DecompressionMethods.GZip;
	}
}