using Twitter.Sampled.Infrastructure.Data.DataModels;

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
                throw new ApplicationException($"[TweetRepository] - {ex.Message}", ex);
            }
        }

    }
}
