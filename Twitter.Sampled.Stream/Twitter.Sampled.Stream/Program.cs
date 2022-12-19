using Microsoft.Extensions.Configuration;
using Twitter.Sampled.Infrastructure.Services;
using Twitter.Sampled.Stream.ConfigModels;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);

var config = builder.Build();

QueueStorageSettings queueStorageSettings = new QueueStorageSettings();
config.GetSection("QueueStorageSettings").Bind(queueStorageSettings);

ITokenService tokenService = new TokenService(config);
ITwitterStreamReader twitterStreamReader = new TwitterStreamReader(config);
IQueueService queueService = new QueueService(queueStorageSettings);

try
{
    var token = await tokenService.GetToken();
    using (var reader = await twitterStreamReader.GetStreamReader(token))
    {
        var tweetString = await reader.ReadLineAsync();

        while (tweetString != null)
        {

            if (!string.IsNullOrEmpty(tweetString))
            {
                // TODO: add log entry

                if (queueService.Exists())
                {
                    await queueService.SendMessage(tweetString);
                }
            }

            tweetString = reader.ReadLine();
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}