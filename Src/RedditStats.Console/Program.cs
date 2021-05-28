using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedditStats.Common;
using static System.Console;

namespace RedditStats.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));

            var advocateService = ServiceCollection.ServiceProvider.GetRequiredService<AdvocateService>();
            var redditApiService = ServiceCollection.ServiceProvider.GetRequiredService<RedditApiService>();

            await foreach (var redditUserName in advocateService.GetRedditUsernames(cancellationTokenSource.Token).ConfigureAwait(false))
            {
                WriteLine($"Reddit User: {redditUserName}");

                await foreach (var response in redditApiService.GetSubmissions(redditUserName, cancellationTokenSource.Token).ConfigureAwait(false))
                {
                    foreach (var child in response.Data.Children)
                    {
                        WriteLine($"\t{DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc)}");
                        WriteLine($"\t{child.Data.LinkPermalink}");
                    }
                }
            }
        }
    }
}
