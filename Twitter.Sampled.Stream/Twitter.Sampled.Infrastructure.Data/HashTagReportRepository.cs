using Microsoft.EntityFrameworkCore;
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

         public IEnumerable<HashTagReport> TopHashTags(int? number)
        {
            return tweetContext.HashTags.GroupBy(ht => ht.Tag).Select(ght => new HashTagReport
            {
                Tag = ght.Key,
                TagCount = ght.Count()
            }).ToHashSet().OrderByDescending(htr => htr.TagCount).Take(number ?? 10);
        }
    }
}
