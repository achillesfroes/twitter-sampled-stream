using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public interface ITweetReportRepository
    {
        Task UpdateTagCount();
        Task<IEnumerable<HashTagReport>> TopHashTags(int number);
    }
}