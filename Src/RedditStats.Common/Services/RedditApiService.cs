using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RedditStats.Common
{
    public class RedditApiService
    {
        readonly IRedditApi _redditApiClient;

        public RedditApiService(IRedditApi redditApi) => _redditApiClient = redditApi;

        public async IAsyncEnumerable<UserListingResponse> GetUserListing(string username, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            UserListingResponse? userListingResponse = null;

            do
            {
                cancellationToken.ThrowIfCancellationRequested();

                var apiResponse = await _redditApiClient.GetUserListing(username, userListingResponse?.Data.After).ConfigureAwait(false);
                await apiResponse.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);

                userListingResponse = apiResponse.Content;

                if (userListingResponse is not null)
                    yield return userListingResponse;

            } while (!string.IsNullOrWhiteSpace(userListingResponse?.Data.After));
        }
    }
}
