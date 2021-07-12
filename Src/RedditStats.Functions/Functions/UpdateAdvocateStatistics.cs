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
        const string _redditUserNameQueue = nameof(GetAdvocates);

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

        [Function(nameof(GetAdvocates)), QueueOutput(_redditUserNameQueue)]
        public async Task<IReadOnlyList<string>> GetAdvocates([TimerTrigger(FunctionsConstants.RunOncePerDayCron, RunOnStartup = FunctionsConstants.ShouldRunOnStartup)] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();
            log.LogInformation($"Running {nameof(UpdateAdvocateStatistics)}");

            var redditUserNames = new List<string>();

            await foreach (var userName in _advocateService.GetRedditUsernames(CancellationToken.None).ConfigureAwait(false))
            {
                log.LogInformation($"Retrieved {userName}");

                redditUserNames.Add(userName);
            }

            return redditUserNames;
        }

        [Function(nameof(UpdateStatistics))]
        public async Task UpdateStatistics([QueueTrigger(_redditUserNameQueue)] string redditUserName, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();

            await foreach (var userListingResponse in _redditApiService.GetSubmissions(redditUserName, CancellationToken.None).ConfigureAwait(false))
            {
                foreach (var child in userListingResponse.Data.Children)
                {
                    log.LogInformation($"Retrived {redditUserName} post from {DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc)}");

                    var advocateSubmission = new RedditSubmission(child.Data);
                    await InsertOrUpdate(_advocateStatisticsDbContext, advocateSubmission).ConfigureAwait(false);
                }
            }

            await _advocateStatisticsDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        static async ValueTask InsertOrUpdate(AdvocateStatisticsDbContext context, RedditSubmission redditSubmittion)
        {
            var existingSubmissions = await context.Submissions.FindAsync(redditSubmittion.RedditUri).ConfigureAwait(false);

            if (existingSubmissions is null)
                await context.AddAsync(redditSubmittion).ConfigureAwait(false);
            else
                context.Entry(existingSubmissions).CurrentValues.SetValues(redditSubmittion);
        }
    }
}
