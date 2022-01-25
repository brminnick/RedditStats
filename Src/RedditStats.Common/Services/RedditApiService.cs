using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RedditStats.Common;

public class RedditApiService
{
	readonly static HttpClient _client = new();

	readonly IRedditApi _redditApiClient;

	public RedditApiService(IRedditApi redditApi) => _redditApiClient = redditApi;

	static RedditApiService() => _client.DefaultRequestHeaders.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue(new System.Net.Http.Headers.ProductHeaderValue(nameof(RedditStats))));

	public async IAsyncEnumerable<UserListingResponse> GetSubmissions(string username, [EnumeratorCancellation] CancellationToken cancellationToken)
	{
		UserListingResponse? userListingResponse = null;

		do
		{
			cancellationToken.ThrowIfCancellationRequested();

#warning Refit crashing during deserialization using .NET 6
			//var response = await _redditApiClient.GetSubmissions(username, cancellationToken, userListingResponse?.Data.After).ConfigureAwait(false);
			//await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

			//userListingResponse = response.Content;

			userListingResponse = await _client.GetFromJsonAsync<UserListingResponse>($"https://api.reddit.com/user/{username}/submitted?after={userListingResponse?.Data.After}").ConfigureAwait(false);

			if (userListingResponse is not null)
				yield return userListingResponse;

		} while (!string.IsNullOrWhiteSpace(userListingResponse?.Data.After));
	}
}
