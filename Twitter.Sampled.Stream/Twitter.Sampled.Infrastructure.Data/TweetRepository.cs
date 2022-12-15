using Microsoft.EntityFrameworkCore;
using Twitter.Sampled.Infrastructure.Data.DataModels;
using System.Linq;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext tweetContext;

        public TweetRepository(TweetContext tweetContext) => this.tweetContext = tweetContext;

        public async Task AddTweet(Tweet tweet)
        {
            try
            {

                await tweetContext.Tweets.AddAsync(tweet);

                await tweetContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
