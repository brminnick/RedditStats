using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using RedditStats.Common;
using Refit;

namespace RedditStats.Functions
{
    public class Program
    {
        readonly static string _storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty;

        static Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddCommandLine(args))
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddHttpClient();

                    services.AddRefitClient<IRedditApi>()
                        .ConfigureHttpClient(client => client.BaseAddress = new Uri(RedditApiConstants.BaseRedditApiUrl))
                        .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
                        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));


                    services.AddSingleton<RedditApiService>();
                })
                .Build();

            return host.RunAsync();

            static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
            static DecompressionMethods getDecompressionMethods() => DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }
    }
}
