using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class TweetReportRepository : ITweetReportRepository
    {
        private readonly TweetContext tweetContext;

        public TweetReportRepository(TweetContext tweetContext) => this.tweetContext = tweetContext;

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
                        throw ex;
                    }
                }
        }

        public async Task<IEnumerable<HashTagReport>> TopHashTags(int number)
        {
            return await tweetContext.HashTagsReport.OrderByDescending(htr => htr.TagCount).Take(number).ToListAsync();
        }
    }
}
