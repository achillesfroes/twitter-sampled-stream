using Twitter.Sampled.Infrastructure.Data;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public class TweetReportService : ITweetReportService
    {
        private readonly IHashTagReportRepository hashTagReportRepository;
        private readonly ITweetReportRepository tweetReportRepository;

        public TweetReportService(
            IHashTagReportRepository hashTagReportRepository,
            ITweetReportRepository tweetReportRepository)
        {
            this.hashTagReportRepository = hashTagReportRepository;
            this.tweetReportRepository = tweetReportRepository;
        }

        public IEnumerable<HashTagReport> GetHashTagReport(int? number)
        {
            if (number == null)
            {
                return new List<HashTagReport>();
            }

            var topHashTags = hashTagReportRepository.TopHashTags(number);

            return topHashTags;
        }

        public async Task<int> GetTweetCount()
        {
            return await tweetReportRepository.GetTweetCount();
        }
    }
}
