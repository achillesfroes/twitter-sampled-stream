using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public interface ITweetReportService
    {
        Task TweetSaved(object sender, EventArgs e);

        Task<IEnumerable<HashTagReport>> GetHashTagReport(int? number = 10);

        Task<int> GetTweetCount();
    }
}