using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using Twitter.Sampled.Application;

namespace Twitter.Sampled.Functions
{
    public class TweetConsumer
    {
        private readonly ITweetService tweetService;
        private readonly ITweetReportService tweetReportService;

        public TweetConsumer(
            ITweetService tweetService,
            ITweetReportService tweetReportService
            )
        {
            this.tweetService = tweetService;
            this.tweetReportService = tweetReportService;
        }

        [FunctionName("QueueTrigger")]
        public async Task Run(
            [QueueTrigger("tweets")] string myQueueItem
            , ILogger log)
        {

            try
            {
                log.LogInformation("[TweetConsumer:Info] - Getting message");

                await tweetService.KeepTweet(myQueueItem);
            }
            catch (Exception ex)
            {

                log.LogError($"[TweetConsumer] - {ex.Message}, StackTrace: {ex.StackTrace}");
            }
        }
    }
}
