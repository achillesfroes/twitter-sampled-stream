using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Twitter.Sampled.Infrastructure.Services;
using Twitter.Sampled.Stream.ConfigModels;

var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
var config = builder.Build();

using ILoggerFactory loggerFactory =
    LoggerFactory.Create(builder =>
        builder.AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss ";
        }));

ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

QueueStorageSettings queueStorageSettings = new QueueStorageSettings();
config.GetSection("QueueStorageSettings").Bind(queueStorageSettings);

using (logger.BeginScope("[scope is enabled]"))
{
    ITokenService tokenService = new TokenService(config);
    ITwitterStreamReader twitterStreamReader = new TwitterStreamReader(config);
    IQueueService queueService = new QueueService(queueStorageSettings);

    try
    {
        var token = await tokenService.GetToken();
        using (var reader = await twitterStreamReader.GetStreamReader(token))
        {
            var tweetString = await reader.ReadLineAsync();
            logger.LogInformation("[StreamReader:Info] - Starting read");
            while (tweetString != null)
            {
                if (!string.IsNullOrEmpty(tweetString))
                {
                    if (queueService.Exists())
                    {
                        logger.LogInformation("[StreamReader:Info] - Sending message");
                        await queueService.SendMessage(tweetString);
                        logger.LogInformation("[StreamReader:Info] - Message sent");
                    }
                }

                tweetString = reader.ReadLine();
            }
        }
    }
    catch (Exception ex)
    {
        logger.LogError($"[StreamReader:Error] - {ex.Message} StackTrace: {ex.StackTrace}");
    } 
}