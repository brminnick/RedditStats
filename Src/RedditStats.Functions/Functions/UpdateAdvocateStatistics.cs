using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class UpdateAdvocateStatistics
    {
        readonly static IReadOnlyList<string> _redditUserNames = new[]
        {
            "brminnick",
            "maximrouiller"
        };

        readonly RedditApiService _redditApiService;
        readonly AdvocateStatisticsDbContext _advocateStatisticsDbContext;

        public UpdateAdvocateStatistics(RedditApiService redditApiService, AdvocateStatisticsDbContext advocateStatisticsDbContext)
        {
            _redditApiService = redditApiService;
            _advocateStatisticsDbContext = advocateStatisticsDbContext;
        }

        [Function(nameof(UpdateAdvocateStatistics))]
        public async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();
            log.LogInformation($"Running {nameof(UpdateAdvocateStatistics)}");

            foreach (var userName in _redditUserNames)
            {
                log.LogInformation($"Retrieving Data for {userName}");

                await foreach (var userListingResponse in _redditApiService.GetSubmissions(userName, CancellationToken.None).ConfigureAwait(false))
                {
                    foreach (var child in userListingResponse.Data.Children)
                    {
                        log.LogInformation($"Retrived {userName} post from {DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc)}");

                        var advocateSubmission = new RedditSubmission(child.Data);
                        await InsertOrUpdate(_advocateStatisticsDbContext, advocateSubmission).ConfigureAwait(false);
                    }
                }
            }

            await _advocateStatisticsDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        static async ValueTask InsertOrUpdate(AdvocateStatisticsDbContext context, RedditSubmission redditSubmittion)
        {
            var existingSubmissions = context.Submissions.Find(redditSubmittion.RedditUri);

            if (existingSubmissions is null)
                await context.AddAsync(redditSubmittion).ConfigureAwait(false);
            else
                context.Entry(existingSubmissions).CurrentValues.SetValues(redditSubmittion);
        }
    }
}
