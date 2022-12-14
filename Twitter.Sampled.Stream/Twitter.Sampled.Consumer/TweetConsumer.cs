using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Sampled.Consumer
{
    public static class TweetConsumer
    {
        [FunctionName("QueueTrigger")]
        public static void Run(
            [QueueTrigger("tweets")] string myQueueItem,
            ILogger log)
        {
            byte[] data = Convert.FromBase64String(myQueueItem);
            string decodedString = Encoding.UTF8.GetString(data);
        }
    }
}
