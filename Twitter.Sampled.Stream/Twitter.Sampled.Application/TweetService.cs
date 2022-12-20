using Microsoft.Extensions.Logging;
using Twitter.Sampled.Application.Extensions;
using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Application
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository tweetRepository;
        private readonly ILogger<TweetService> logger;

        public TweetService(
            ITweetRepository tweetRepository
            , ILogger<TweetService> logger)
        {
            this.tweetRepository = tweetRepository;
            this.logger = logger;
        }

        public async Task KeepTweet(string tweetString)
        {
            logger.LogInformation("[TweetService] - Starting KeepTweet");

            var tweetData = tweetString.GetDecodedTweet();

            if (tweetData != null)
            {
                var hashtags = tweetData.Data.Entities.Hashtags.Select(ht => ht.Tag).ToList();

                logger.LogInformation("[TweetService] - Hashtags retrived");

                if (hashtags.Count != 0)
                {

                    logger.LogInformation("[TweetService] - Starting tweet mapping");
                    var tweetId = Convert.ToUInt64(tweetData.Data.Id);

                    var tweet = new Tweet
                    {
                        Id = tweetId,
                        Text = tweetData.Data.Text,
                        Lang = tweetData.Data.Lang,
                        ImpressionCount = tweetData.Data.PromotedMetrics.ImpressionCount,
                        RetweetCount = tweetData.Data.PublicMetrics.RetweetCount + tweetData.Data.PromotedMetrics.RetweetCount,
                        HashTags = tweetData.Data.Entities.Hashtags.Select(t => new HashTag { TweetId = tweetId, Tag = t.Tag }).ToList()
                    };
                    logger.LogInformation("[TweetService] - Ended tweet mapping");
                    await tweetRepository.AddTweet(tweet);
                }
            }

            logger.LogInformation("[TweetService] - Ended KeepTweet");
        }
    }
}
