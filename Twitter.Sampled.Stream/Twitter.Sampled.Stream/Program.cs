using Microsoft.Extensions.Configuration;
using Twitter.Sampled.Models;
using Twitter.Sampled.Token;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();

ITokenService tokenService = new TokenService(config);

var token = await tokenService.GetToken();

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
var streamAsync = Task.Run(() => client.GetStreamAsync("https://api.twitter.com/2/tweets/sample/stream?tweet.fields=author_id,entities,lang,promoted_metrics,text")).Result;

using (var reader = new StreamReader(streamAsync))
{
    var tweetString = reader.ReadLine();
    while (tweetString != null)
    {
        tweetString = reader.ReadLine();
        var tweet = Newtonsoft.Json.JsonConvert.DeserializeObject<TweetData>(tweetString);

        if (tweet != null && (tweet.Data.Lang != "ja" || tweet.Data.Lang != "zh"))
        {
            Console.WriteLine(tweet.Data.Text);
        }
    }
}