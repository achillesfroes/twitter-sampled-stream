namespace FunctionApp.TwitterStreamReader;

public class TweetData
{ 
    public Tweet Data { get; set; }
}

public class Tweet
{
    public string Id { get; set; }
    public string Text { get; set; }
}

