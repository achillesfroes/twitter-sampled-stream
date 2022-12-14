using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp.TwitterStreamReader
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [UrlStream(Url = "https://api.twitter.com/2/tweets/sample/stream")] TweetData tweetData)
        {
            Console.WriteLine(tweetData.Data.Text);
        }
    }
}
