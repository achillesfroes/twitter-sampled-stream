using Microsoft.EntityFrameworkCore;
using System.Xml;
using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class TweetRepository : ITweetRepository
    {
        private readonly TweetContext tweetContext;

        public TweetRepository(TweetContext tweetContext) => this.tweetContext = tweetContext;

        public async Task AddTweet(Tweet tweet) {

            
            tweetContext.HashTags.AddRangeIfNotExists(tweet.Tags, t => t.Tag);

            if (tweetContext.Tweets.Any(e => e.Id == tweet.Id))
            {
                tweetContext.Tweets.Attach(tweet).State = EntityState.Modified;
            }
            else
            {
                await tweetContext.Tweets.AddAsync(tweet);
            }

            await tweetContext.SaveChangesAsync();
        }

    }
}
