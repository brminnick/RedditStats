using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RedditStats.Common;

namespace RedditStats.Functions
{
    class UpdateAdvocateStatistics
    {
        readonly AdvocateService _advocateService;
        readonly RedditApiService _redditApiService;
        readonly AdvocateStatisticsDbContext _advocateStatisticsDbContext;

        public UpdateAdvocateStatistics(AdvocateService advocateService,
                                        RedditApiService redditApiService,
                                        AdvocateStatisticsDbContext advocateStatisticsDbContext)
        {
            _advocateService = advocateService;
            _redditApiService = redditApiService;
            _advocateStatisticsDbContext = advocateStatisticsDbContext;
        }

        [Function(nameof(UpdateAdvocateStatistics))]
        public async Task Run([TimerTrigger(FunctionsConstants.RunOncePerDayCron, RunOnStartup = FunctionsConstants.ShouldRunOnStartup)] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();
            log.LogInformation($"Running {nameof(UpdateAdvocateStatistics)}");

            await foreach (var userName in _advocateService.GetRedditUsernames(CancellationToken.None).ConfigureAwait(false))
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
