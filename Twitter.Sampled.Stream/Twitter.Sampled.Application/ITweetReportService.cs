using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application
{
    public interface ITweetReportService
    {
        IEnumerable<HashTagReport> GetHashTagReport(int? number);

        Task<int> GetTweetCount();
    }
}