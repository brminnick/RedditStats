using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using RedditStats.Common;
using Refit;

namespace RedditStats.Functions
{
    public class Program
    {
        readonly static string _storageConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage") ?? string.Empty;
        readonly static string _databaseConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString") ?? string.Empty;

        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await InitializeDatabase(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddCommandLine(args))
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureLogging(logBuilder => logBuilder.SetMinimumLevel(LogLevel.Error)) //https://github.com/Azure/azure-functions-dotnet-worker/issues/417
                .ConfigureServices(services =>
                {
                    // HttpClients
                    services.AddHttpClient();

                    // Logging
                    services.AddLogging();

                    // DbContexts
                    services.AddDbContext<AdvocateStatisticsDbContext>(options => options.UseSqlServer(_databaseConnectionString));

                    // Refit APIs
                    services.AddRefitClient<IRedditApi>()
                        .ConfigureHttpClient(client => client.BaseAddress = new Uri(RedditApiConstants.BaseRedditApiUrl))
                        .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
                        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

                    services.AddRefitClient<IAdvocateApi>()
                        .ConfigureHttpClient(client => client.BaseAddress = new Uri(AdvocateConstants.BaseAdvocateApi))
                        .ConfigurePrimaryHttpMessageHandler(_ => new HttpClientHandler { AutomaticDecompression = getDecompressionMethods() })
                        .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(3, sleepDurationProvider));

                    // Services
                    services.AddSingleton<AdvocateService>();
                    services.AddSingleton<RedditApiService>();
                });

            static TimeSpan sleepDurationProvider(int attemptNumber) => TimeSpan.FromSeconds(Math.Pow(2, attemptNumber));
            static DecompressionMethods getDecompressionMethods() => DecompressionMethods.Deflate | DecompressionMethods.GZip;
        }

        static async Task InitializeDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<AdvocateStatisticsDbContext>();
                await DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
            }
        }
    }
}
