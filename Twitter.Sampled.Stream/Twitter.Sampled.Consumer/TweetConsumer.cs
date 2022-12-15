using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;
using Twitter.Sampled.Application;

namespace Twitter.Sampled.Consumer
{
    public class TweetConsumer
    {
        private readonly ITweetService tweetService;
        private readonly ITweetReportService tweetReportService;
        //private readonly ILogger log;

        public TweetConsumer(
            ITweetService tweetService,
            ITweetReportService tweetReportService
            //, ILogger log
            )
        {
            this.tweetService = tweetService;
            this.tweetReportService = tweetReportService;
            //this.log = log;
        }

        [FunctionName("QueueTrigger")]
        public async Task Run(
            [QueueTrigger("tweets")] string myQueueItem
            , ILogger log)
        {
            byte[] data = Convert.FromBase64String(myQueueItem);
            string decodedTweet = Encoding.UTF8.GetString(data);

            tweetService.TweetSaved += tweetReportService.TweetSaved;
            await tweetService.KeepTweet(decodedTweet);
        }
    }
}
