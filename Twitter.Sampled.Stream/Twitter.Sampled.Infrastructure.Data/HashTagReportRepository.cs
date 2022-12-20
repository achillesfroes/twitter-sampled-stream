using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class HashTagReportRepository : IHashTagReportRepository
    {
        private readonly TweetContext tweetContext;

        public HashTagReportRepository(TweetContext tweetContext)
        {
            this.tweetContext = tweetContext;
        }

         public IEnumerable<Models.HashTagReport> TopHashTags(int? number)
        {
            var hashTagReports =  tweetContext.HashTags.GroupBy(ht => ht.Tag).Select(ght => new Models.HashTagReport
            {
                Tag = ght.Key,
                TagCount = ght.Count()
            }).ToHashSet().OrderByDescending(htr => htr.TagCount).Take(number ?? 10);

            return hashTagReports.Select(tht => new Models.HashTagReport
            {
                Tag = tht.Tag,
                TagCount = tht.TagCount,
            }).ToList();
        }
    }
}
