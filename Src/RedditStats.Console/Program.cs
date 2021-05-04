using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedditStats.Common;

namespace RedditStats.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            var redditApiService = ServiceCollection.ServiceProvider.GetRequiredService<RedditApiService>();

            await foreach (var response in redditApiService.GetSubmissions("brminnick", cancellationToken.Token).ConfigureAwait(false))
            {
                foreach (var child in response.Data.Children)
                {
                    System.Console.WriteLine(DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc));
                    System.Console.WriteLine(child.Data.LinkPermalink);
                }
            }
        }
    }
}
