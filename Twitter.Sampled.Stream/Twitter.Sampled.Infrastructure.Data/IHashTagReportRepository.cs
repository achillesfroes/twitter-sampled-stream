namespace Twitter.Sampled.Infrastructure.Data
{
    public interface IHashTagReportRepository
    {
        IEnumerable<Models.HashTagReport> TopHashTags(int? number);
    }
}