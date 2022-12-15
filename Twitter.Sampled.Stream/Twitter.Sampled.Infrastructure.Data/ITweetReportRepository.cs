using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public interface ITweetReportRepository
    {
        Task<int> GetTweetCount();
    }
}