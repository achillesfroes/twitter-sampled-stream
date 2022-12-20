using FakeItEasy;
using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application.Test
{
    public class TweetReportServiceTests
    {
        [Test]
        public void GetHashTagReport_Top10_ShouldGetTop10HashTags()
        {
            // arrange
            var hashTagReportRepository = A.Fake<IHashTagReportRepository>();
            var tweetReportRepository = A.Fake<ITweetReportRepository>();
            int? topTags = 10;
            var hashTags =  A.CollectionOfFake<HashTagReport>(10).AsEnumerable();
            A.CallTo(() => hashTagReportRepository.TopHashTags(A<int>.That.IsEqualTo(10))).Returns(hashTags);

            // act
            var tweetReportService = new TweetReportService(hashTagReportRepository, tweetReportRepository);
            var getHashTagReport = tweetReportService.GetHashTagReport(topTags);

            // assert
            A.CallTo(() => hashTagReportRepository.TopHashTags(A<int>.That.IsEqualTo(10))).MustHaveHappened();
        }

        [Test]
        public void GetHashTagReport_None_ShouldGetEmptyHashTagReportColletion()
        {
            // arrange
            var hashTagReportRepository = A.Fake<IHashTagReportRepository>();
            var tweetReportRepository = A.Fake<ITweetReportRepository>();
            int? topTags = null;
            var hashTags = A.CollectionOfFake<HashTagReport>(10).AsEnumerable();

            // act
            var tweetReportService = new TweetReportService(hashTagReportRepository, tweetReportRepository);
            var getHashTagReport = tweetReportService.GetHashTagReport(topTags);

            // assert
            A.CallTo(() => hashTagReportRepository.TopHashTags(A<int?>.That.IsEqualTo(null))).MustNotHaveHappened();
            Assert.That(getHashTagReport, Is.Empty);

        }

        [Test]
        public async Task GetTweetCount_ShouldCallGetTweetCount()
        {
            // arrange
            var hashTagReportRepository = A.Fake<IHashTagReportRepository>();
            var tweetReportRepository = A.Fake<ITweetReportRepository>();

            // act
            var tweetReportService = new TweetReportService(hashTagReportRepository, tweetReportRepository);
            var getTweetCount = await tweetReportService.GetTweetCount();

            // assert
            A.CallTo(() => tweetReportRepository.GetTweetCount()).MustHaveHappened();
            Assert.That(getTweetCount, Is.TypeOf(typeof(int)));

        }
    }
}
