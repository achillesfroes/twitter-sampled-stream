using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public interface ITweetReportService
    {
        //Task TweetSaved(object sender, EventArgs e);

        IEnumerable<HashTagReport> GetHashTagReport(int? number);

        Task<int> GetTweetCount();
    }
}