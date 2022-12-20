namespace Twitter.Sampled.Application
{
    public interface ITweetService
    {
        Task KeepTweet(string tweetString);
    }
}