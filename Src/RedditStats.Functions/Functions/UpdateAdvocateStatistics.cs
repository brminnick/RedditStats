using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace RedditStats.Functions
{
    public class UpdateAdvocateStatistics
    {
        [Function(nameof(UpdateAdvocateStatistics))]
        public async Task Run([TimerTrigger("0 0 0 * * *")] TimerInfo myTimer, FunctionContext context)
        {
            var log = context.GetLogger<UpdateAdvocateStatistics>();
            log.LogInformation($"Running {nameof(UpdateAdvocateStatistics)}");


        }
    }
}
