using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RedditStats.Common;
using static System.Console;

namespace RedditStats.Console;

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
					var advocateSubmission = new RedditSubmission(child.Data);

					WriteLine($"\t{advocateSubmission.Author}");
					WriteLine($"\t{advocateSubmission.CommentCount}");
					WriteLine($"\t{advocateSubmission.DownVotes}");
					WriteLine($"\t{advocateSubmission.IsAwarded}");
					WriteLine($"\t{advocateSubmission.RedditUri}");
					WriteLine($"\t{advocateSubmission.SubmittedAt}");
					WriteLine($"\t{advocateSubmission.Subreddit}");
					WriteLine($"\t{advocateSubmission.Title}");
					WriteLine($"\t{advocateSubmission.UpdatedAt}");
					WriteLine($"\t{advocateSubmission.UpVoteRatio}");
					WriteLine($"\t{advocateSubmission.UpVotes}");
				}
			}
		}
	}
}