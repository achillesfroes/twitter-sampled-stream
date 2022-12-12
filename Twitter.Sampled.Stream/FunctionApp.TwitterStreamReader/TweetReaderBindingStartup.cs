using FunctionApp.TwitterStreamReader;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(TweetReaderBindingStartup))]
namespace FunctionApp.TwitterStreamReader
{
    public class TweetReaderBindingStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddTweetReaderBinding();
        }
    }
}
