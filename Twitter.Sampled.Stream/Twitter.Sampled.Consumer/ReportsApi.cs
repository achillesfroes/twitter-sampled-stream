using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Twitter.Sampled.Application;

namespace Twitter.Sampled.Functions
{
    public class ReportsApi
    {
        private readonly ITweetReportService tweetReportService;

        public ReportsApi(ITweetReportService tweetReportService)
        {
            this.tweetReportService = tweetReportService;
        }

        [FunctionName("TopTags")]
        public async Task<IActionResult> RunTopTags(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "twitter-sampled/tags/top/{number:int?}")] HttpRequest req,
            int? number,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var hashTagReport = tweetReportService.GetHashTagReport(number);

            return new OkObjectResult(hashTagReport);
        }

        [FunctionName("TweetsCount")]
        public async Task<IActionResult> RunTweetsCount(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "twitter-sampled/count")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var tweetCount = await tweetReportService.GetTweetCount();

            return new OkObjectResult(tweetCount);
        }
    }
}
