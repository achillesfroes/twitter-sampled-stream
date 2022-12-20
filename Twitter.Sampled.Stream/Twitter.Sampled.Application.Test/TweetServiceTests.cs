using FakeItEasy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task KeepTweet_GoodTweet_ShouldAddTweet()
        {
            // arrange
            var tweetRepository = A.Fake<ITweetRepository>();
            var tweetServiceLog = A.Fake<ILogger<TweetService>>();
            string tweetJson = JsonConvert.SerializeObject(new TweetData()
            {
                Data = new Tweet
                {
                    Text = A.Dummy<string>(),
                    Entities = new Entities
                    {
                        Hashtags = new List<Hashtag> {
                                new Hashtag { Tag = "ATag" }
                            }
                    }
                }
            });
            var tweetUncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(tweetJson));

            // act
            var tweetService = new TweetService(tweetRepository, tweetServiceLog);
            await tweetService.KeepTweet(tweetUncoded);

            // assert
            A.CallTo(() => tweetRepository.AddTweet(A<Twitter.Sampled.Infrastructure.Data.DataModels.Tweet>.That.IsInstanceOf(typeof(Twitter.Sampled.Infrastructure.Data.DataModels.Tweet)))).MustHaveHappened();

        }

        [Test]
        public async Task KeepTweet_EmptyTweet_ShouldNotAddTweet()
        {
            // arrange
            var tweetRepository = A.Fake<ITweetRepository>();
            var tweetServiceLog = A.Fake<ILogger<TweetService>>();
            string tweetJson = "";
            var tweetUncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(tweetJson));

            // act
            var tweetService = new TweetService(tweetRepository, tweetServiceLog);
            await tweetService.KeepTweet(tweetUncoded);

            // assert
            A.CallTo(() => tweetRepository.AddTweet(A<Twitter.Sampled.Infrastructure.Data.DataModels.Tweet>.That.IsInstanceOf(typeof(Twitter.Sampled.Infrastructure.Data.DataModels.Tweet)))).MustNotHaveHappened();

        }

        [Test]
        public async Task KeepTweet_TweetWithoutTags_ShouldNotAddTweet()
        {
            // arrange
            var tweetRepository = A.Fake<ITweetRepository>();
            var tweetServiceLog = A.Fake<ILogger<TweetService>>();
            string tweetJson = JsonConvert.SerializeObject(new TweetData()
            {
                Data = new Tweet
                {
                    Text = A.Dummy<string>()
                }
            });
            var tweetUncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(tweetJson));

            // act
            var tweetService = new TweetService(tweetRepository, tweetServiceLog);
            await tweetService.KeepTweet(tweetUncoded);

            // assert
            A.CallTo(() => tweetRepository.AddTweet(A<Twitter.Sampled.Infrastructure.Data.DataModels.Tweet>.That.IsInstanceOf(typeof(Twitter.Sampled.Infrastructure.Data.DataModels.Tweet)))).MustNotHaveHappened();

        }
    }
}