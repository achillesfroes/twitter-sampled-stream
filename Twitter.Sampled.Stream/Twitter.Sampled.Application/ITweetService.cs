namespace Twitter.Sampled.Application
{
    public interface ITweetService
    {
        event TweetService.TweetSavedEventHandler TweetSaved;

        Task KeepTweet(string tweetString);
    }
}