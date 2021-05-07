using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RedditStats.Functions
{
    public class UpdateAdvocateStatistics
    {
        //[Function(nameof(UpdateAdvocateStatistics))]
        public static void Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();
            log.LogInformation($"Running {nameof(UpdateAdvocateStatistics)}");

            throw new NotImplementedException();
        }
    }
}
