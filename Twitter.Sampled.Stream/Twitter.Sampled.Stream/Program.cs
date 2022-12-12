using System.Text.RegularExpressions;
using Newtonsoft.Json;

string bearerToken = "AAAAAAAAAAAAAAAAAAAAAFvEjgEAAAAACYqzBKBAJGNljHBzTKA9CKYfso0%3DQ6VrCYSzmIw81gG49Fqg6DxebafWW7BkZxJn6HgU1sz3CKARLi";


HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
//var streamAsync = await client.GetStreamAsync("https://api.twitter.com/2/tweets/sample/stream");
var streamAsync = Task.Run(() => client.GetStreamAsync("https://api.twitter.com/2/tweets/sample/stream")).Result;

using (var reader = new StreamReader(streamAsync))
{
    //var twitte = await reader.ReadLineAsync();
    var tweetString = reader.ReadLine();
    while (tweetString != null)
    {
        //Console.WriteLine(tweetString);
        tweetString = reader.ReadLine();
        var tweet = Newtonsoft.Json.JsonConvert.DeserializeObject<TweetData>(tweetString);

        if (tweet.Data.HashTags.Any())
        {
            Console.WriteLine(string.Join(",", tweet.Data.HashTags));
        }

        //if (tweet.Data.Text.Contains("#"))
        //{
        //    Console.WriteLine(tweet.Data.Text);
        //}
        
    }
}

public class TweetData
{
    //[JsonProperty("data")]
    public Tweet Data { get; set; }
}


public class Tweet
{
    private readonly Regex regex = new Regex(@"\#\w+");

    public string Id { get; set; }
    public string Text { get; set; }

    public List<string> HashTags
    {
        get
        {
            var matches = regex.Matches(Text);

            if (matches.Any())
                return regex.Matches(Text).Select(r => r.Value).ToList();

            return new List<string>();
        }
    }
    public object Entities { get; set; }
    public string Lang { get; set; }
    public object Non_Public_Metrics { get; set; }
    public object Promoted_Metrics { get; set; }
    public object Public_Metrics { get; set; }
    public string Source { get; set; }
}