using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Infrastructure.Data.DataModels;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository tweetRepository;

        public delegate void TweetSavedEventHandler(object sender, EventArgs eventArgs);

        public event TweetSavedEventHandler TweetSaved;

        public TweetService(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
        }
        public async Task KeepTweet(string tweetString)
        {
            var tweetData = Newtonsoft.Json.JsonConvert.DeserializeObject<TweetData>(tweetString);

            if (tweetData != null)
            {
                var tweet = new Infrastructure.Data.DataModels.Tweet
                {
                    Id = Convert.ToUInt64(tweetData.Data.Id),
                    Text = tweetData.Data.Text,
                    ImpressionCount = tweetData.Data.PromotedMetrics.ImpressionCount,
                    RetweetCount = tweetData.Data.PublicMetrics.RetweetCount + tweetData.Data.PromotedMetrics.RetweetCount,
                    Tags = tweetData.Data.Entities.Hashtags.Select(t => new HashTag { Tag = t.Tag }).ToList()
                };

                await tweetRepository.AddTweet(tweet);

                TweetSaved?.Invoke(this, new EventArgs());
            }
            
        }
    }
}
