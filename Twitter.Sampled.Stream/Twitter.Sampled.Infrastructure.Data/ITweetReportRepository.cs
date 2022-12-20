namespace Twitter.Sampled.Infrastructure.Data
{
    public interface ITweetReportRepository
    {
        Task<int> GetTweetCount();
    }
}