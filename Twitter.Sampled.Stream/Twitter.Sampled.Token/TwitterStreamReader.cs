using Microsoft.Extensions.Configuration;

namespace Twitter.Sampled.Infrastructure.Services
{
    public class TwitterStreamReader : ITwitterStreamReader
    {
        private readonly Uri streamURL;
        private string streamURLDefaultValue = "https://api.twitter.com/2/tweets/sample/stream?tweet.fields=author_id,entities,lang,promoted_metrics,text";

        public TwitterStreamReader(IConfigurationRoot configurationRoot)
        {
            if (configurationRoot != null)
            {
                streamURL = new Uri(configurationRoot["StreamReanderSettings:URL"] ?? streamURLDefaultValue);
            }
        }

        public async Task<StreamReader> GetStreamReader(Token token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.AccessToken}");
            var streamAsync = await client.GetStreamAsync(streamURL);

            return new StreamReader(streamAsync);
        }
    }
}
