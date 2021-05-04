using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using RedditStats.Common;
using Refit;

namespace RedditStats.Functions
{
    class GetAdvocateSubmissions
    {
        readonly static IReadOnlyList<string> _redditUserNames = new[]
        {
            "brminnick",
            "maximrouiller"
        };

        readonly RedditApiService _redditApiService;

        public GetAdvocateSubmissions(RedditApiService redditApiService) => _redditApiService = redditApiService;

        [Function(nameof(GetAdvocateSubmissions))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, FunctionContext context)
        {
            var log = context.GetLogger<GetAdvocateSubmissions>();
            log.LogInformation("Retrieving Advocate Reddit Post Statistics");

            var cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(10));
            var redditDataDictionary = new Dictionary<string, List<UserListingResponse>>();

            try
            {
                foreach (var userName in _redditUserNames)
                {
                    log.LogInformation($"Retrieving Data for {userName}");
                    redditDataDictionary.Add(userName, new List<UserListingResponse>());

                    await foreach (var userListingResponse in _redditApiService.GetSubmissions(userName, cancellationToken.Token).ConfigureAwait(false))
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
            catch (ApiException exception)
            {
                var response = req.CreateResponse(exception.StatusCode);
                response.Headers = new HttpHeadersCollection(exception.Headers);

                if (exception.Content is not null)
                    await response.WriteStringAsync(exception.Content).ConfigureAwait(false);

                return response;
            }
        }
    }
}
