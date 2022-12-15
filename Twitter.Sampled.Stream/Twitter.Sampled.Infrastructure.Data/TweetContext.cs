using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.PortableExecutable;
using Twitter.Sampled.Infrastructure.Data.DataModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public DbSet<HashTagReport> HashTagsReport { get; set; }
        public DbSet<TweetReport> TweetsReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tweet>()
            .HasMany(c => c.HashTags)
            .WithOne(e => e.Tweet);
        }

    }
}
