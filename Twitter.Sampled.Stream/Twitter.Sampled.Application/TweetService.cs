using System.Text.RegularExpressions;
using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Infrastructure.Data.DataModels;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public class TweetService : ITweetService
    {
        private readonly ITweetRepository tweetRepository;
        private readonly Regex onlyUtf8Characters;

        public delegate Task TweetSavedEventHandler(object sender, EventArgs eventArgs);

        public event TweetSavedEventHandler? TweetSaved;

        public TweetService(ITweetRepository tweetRepository)
        {
            this.tweetRepository = tweetRepository;
            onlyUtf8Characters = new Regex(@"\b(\p{IsGreek}+(\s)?)+\p{Pd}\s(\p{IsBasicLatin}+(\s)?)+");
        }

        public async Task KeepTweet(string tweetString)
        {
            var tweetData = Newtonsoft.Json.JsonConvert.DeserializeObject<TweetData>(tweetString);

            if (tweetData != null && !onlyUtf8Characters.IsMatch(tweetData.Data.Text))
            {
                var tweetId = Convert.ToUInt64(tweetData.Data.Id);

                var tweet = new Infrastructure.Data.DataModels.Tweet
                {
                    Id = tweetId,
                    Text = tweetData.Data.Text,
                    Lang = tweetData.Data.Lang,
                    ImpressionCount = tweetData.Data.PromotedMetrics.ImpressionCount,
                    RetweetCount = tweetData.Data.PublicMetrics.RetweetCount + tweetData.Data.PromotedMetrics.RetweetCount,
                    HashTags = tweetData.Data.Entities.Hashtags.Select(t => new HashTag { TweetId = tweetId, Tag = t.Tag }).ToList()
                };

                await tweetRepository.AddTweet(tweet);

                TweetSaved?.Invoke(this, new EventArgs());
            }

        }
    }
}
