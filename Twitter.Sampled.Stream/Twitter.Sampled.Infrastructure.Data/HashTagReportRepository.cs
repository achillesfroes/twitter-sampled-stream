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
                    HashTagReport? hashTagReport = await tweetContext.HashTagsReport.FirstOrDefaultAsync(htr => htr.Tag == hashTag.Tag);

                    try
                    {
                        if (hashTagReport != null)
                        {
                            hashTagReport.TagCount = hashTag.TagCount;

                            tweetContext.Entry(hashTagReport).State = EntityState.Modified;
                        }
                        else
                        {
                            await tweetContext.HashTagsReport.AddAsync(hashTag);
                        }

                        await tweetContext.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
        }

        public async Task<IEnumerable<HashTagReport>> TopHashTags(int? number)
        {
            return await tweetContext.HashTagsReport.OrderByDescending(htr => htr.TagCount).Take(number ?? 10).ToListAsync();
        }
    }
}
