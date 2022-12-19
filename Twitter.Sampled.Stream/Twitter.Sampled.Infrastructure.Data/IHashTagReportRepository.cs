using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public interface IHashTagReportRepository
    {
        //Task UpdateTagCount();
        IEnumerable<HashTagReport> TopHashTags(int? number);
    }
}