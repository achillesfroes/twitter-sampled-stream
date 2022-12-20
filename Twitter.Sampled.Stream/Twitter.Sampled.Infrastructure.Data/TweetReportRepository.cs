using Microsoft.EntityFrameworkCore;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class TweetReportRepository : ITweetReportRepository
    {
        private readonly TweetContext tweetContext;

        public TweetReportRepository(TweetContext tweetContext) => this.tweetContext = tweetContext;


        public async Task<int> GetTweetCount()
        {
            return await tweetContext.Tweets.CountAsync();
        }
    }
}
