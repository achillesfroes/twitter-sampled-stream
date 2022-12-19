using Microsoft.EntityFrameworkCore;
using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class HashTagReportRepository : IHashTagReportRepository
    {
        private readonly TweetContext tweetContext;

        public HashTagReportRepository(TweetContext tweetContext) => this.tweetContext = tweetContext;

        public async Task UpdateTagCount()
        {
            
                var tagCount = tweetContext.HashTags.GroupBy(ht => ht.Tag).Select(ght => new HashTagReport
                {
                    Tag = ght.Key,
                    TagCount = ght.Count()
                }).ToList();


                foreach (var hashTag in tagCount)
                {
                    HashTagReport? hashTagReport = await tweetContext.HashTagsReport.FirstOrDefaultAsync(htr => htr.Tag.ToLowerInvariant() == hashTag.Tag.ToLowerInvariant());

                    try
                    {
                        if (hashTagReport != null)
                        {
                            Console.WriteLine("Trying to update");
                            hashTagReport.TagCount = hashTag.TagCount;
                            tweetContext.Entry(hashTagReport).State = EntityState.Modified;
                        }
                        else
                        {
                            Console.WriteLine("Trying to insert");
                            await tweetContext.HashTagsReport.AddAsync(hashTag);
                        }

                        await tweetContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
        }

        public async Task<IEnumerable<HashTagReport>> TopHashTags(int? number)
        {
            return await tweetContext.HashTagsReport.OrderByDescending(htr => htr.TagCount).Take(number ?? 10).ToListAsync();
        }
    }
}
