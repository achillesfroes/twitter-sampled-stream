using Microsoft.EntityFrameworkCore;
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

        public DbSet<HashTagReport> HashTagsReport { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tweet>()
            .HasMany(c => c.HashTags)
            .WithOne(e => e.Tweet);


            //modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");
        }

    }
}
