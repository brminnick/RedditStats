using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class GetAdvocateStatistics
    {
        readonly static IReadOnlyList<string> _redditUserNames = new[]
        {
            "brminnick",
            "maximrouiller"
        };

        readonly RedditApiService _redditApiService;

        public GetAdvocateStatistics(RedditApiService redditApiService) => _redditApiService = redditApiService;

        [Function(nameof(GetAdvocateStatistics))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, FunctionContext context)
        {
            var log = context.GetLogger<GetAdvocateStatistics>();
            log.LogInformation("Retrieving Advocate Reddit Post Statistics");

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(1));
            var redditDataDictionary = new Dictionary<string, List<UserListingResponse>>();

            foreach (var userName in _redditUserNames)
            {
                log.LogInformation($"Retrieving Data for {userName}");
                redditDataDictionary.Add(userName, new List<UserListingResponse>());

                await foreach (var userListingResponse in _redditApiService.GetUserListing(userName, cancellationToken.Token).ConfigureAwait(false))
                {
                    foreach (var child in userListingResponse.Data.Children)
                    {
                        log.LogInformation($"Retrived {userName} post from {DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc)}");
                        redditDataDictionary[userName].Add(child);
                    }
                }
            }

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(redditDataDictionary).ConfigureAwait(false);

            return response;
        }
    }
}
