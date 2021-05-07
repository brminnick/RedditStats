using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RedditStats.Common;
using Refit;

namespace RedditStats.Functions
{
    class GetAdvocateSubmissions
    {  
        readonly AdvocateStatisticsDbContext _advocateStatisticsDbContext;

        public GetAdvocateSubmissions(AdvocateStatisticsDbContext advocateStatisticsDbContext) => _advocateStatisticsDbContext = advocateStatisticsDbContext;

        [Function(nameof(GetAdvocateSubmissions))]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req, FunctionContext context)
        {
            var log = context.GetLogger<GetAdvocateSubmissions>();
            log.LogInformation("Retrieving Advocate Reddit Post Statistics");

            IReadOnlyList<RedditSubmission> advocateSubmissions = await _advocateStatisticsDbContext.Submissions.ToListAsync();

            var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(advocateSubmissions).ConfigureAwait(false);

            return response;
        }
    }
}
