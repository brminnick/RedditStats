using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RedditStats.Common;

public class RedditApiService
{
	readonly IRedditApi _redditApiClient;

	public RedditApiService(IRedditApi redditApi) => _redditApiClient = redditApi;

	public async IAsyncEnumerable<UserListingResponse> GetSubmissions(string username, [EnumeratorCancellation] CancellationToken cancellationToken)
	{
		UserListingResponse? userListingResponse = null;

		do
		{
			cancellationToken.ThrowIfCancellationRequested();

			var response = await _redditApiClient.GetSubmissions(username, cancellationToken, userListingResponse?.Data.After).ConfigureAwait(false);
			await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

			userListingResponse = response.Content;

			if (userListingResponse is not null)
				yield return userListingResponse;

		} while (!string.IsNullOrWhiteSpace(userListingResponse?.Data.After));
	}
}