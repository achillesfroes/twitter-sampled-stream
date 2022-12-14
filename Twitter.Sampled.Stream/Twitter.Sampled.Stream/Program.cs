using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Text;
using Twitter.Sampled.Models;
using Twitter.Sampled.Stream.ConfigModels;
using Twitter.Sampled.Token;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();

ITokenService tokenService = new TokenService(config);

var token = await tokenService.GetToken();

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
var streamAsync = Task.Run(() => client.GetStreamAsync("https://api.twitter.com/2/tweets/sample/stream?tweet.fields=author_id,entities,lang,promoted_metrics,text")).Result;

QueueStorageSettings queueStorageSettings = new QueueStorageSettings();

config.GetSection("QueueStorageSettings").Bind(queueStorageSettings);

QueueClient queueClient = new QueueClient(queueStorageSettings.ConnectionString, queueStorageSettings.QueueName, new QueueClientOptions
{
    MessageEncoding = QueueMessageEncoding.Base64
});

//QueueClient queueClient = new QueueClient(queueStorageSettings.ConnectionString, queueStorageSettings.QueueName);


queueClient.CreateIfNotExists();

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

            if (queueClient.Exists())
            {
                var bytes = Encoding.UTF8.GetBytes(tweetString);
                queueClient.SendMessage(Convert.ToBase64String(bytes));
            }
        }
    }
}