using Twitter.Sampled.Infrastructure.Data.DataModels;

namespace Twitter.Sampled.Infrastructure.Data
{
    public interface ITweetRepository
    {
        Task AddTweet(Tweet tweet);
    }
}