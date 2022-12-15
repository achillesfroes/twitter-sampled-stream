using Twitter.Sampled.Infrastructure.Data;

namespace Twitter.Sampled.Application
{
    public class TweetReportService : ITweetReportService
    {
        private readonly ITweetReportRepository tweetReportRepository;

        public TweetReportService(ITweetReportRepository tweetReportRepository) => this.tweetReportRepository = tweetReportRepository;

        public async Task TweetSaved(object sender, EventArgs e)
        {
            await tweetReportRepository.UpdateTagCount();

            var topHashTags = await tweetReportRepository.TopHashTags(10);

            Console.WriteLine(String.Join(",", topHashTags.Select(tht => $"{tht.Tag} : {tht.TagCount}")) );
        }
    }
}
