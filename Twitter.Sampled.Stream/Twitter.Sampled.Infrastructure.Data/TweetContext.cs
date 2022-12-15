using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.PortableExecutable;
using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public class TweetContext : DbContext
    {
        public TweetContext(DbContextOptions<TweetContext> options)
            : base(options)
        {
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<HashTag> HashTags { get; set; }

        public DbSet<HashTagReport> MyProperty { get; set; }
    }
}
