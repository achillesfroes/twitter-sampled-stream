using Microsoft.Azure.WebJobs.Host.Config;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace FunctionApp.TwitterStreamReader
{
    public class TweetReaderBinding : IExtensionConfigProvider
    {
        private readonly HttpClient _httpClient;
        private string bearerToken = "AAAAAAAAAAAAAAAAAAAAAFvEjgEAAAAACYqzBKBAJGNljHBzTKA9CKYfso0%3DQ6VrCYSzmIw81gG49Fqg6DxebafWW7BkZxJn6HgU1sz3CKARLi";

        public TweetReaderBinding()
        {
            _httpClient = new HttpClient();
        }
        public void Initialize(ExtensionConfigContext context)
        {
            var rule = context.AddBindingRule<UrlStreamAttribute>();
            rule.BindToStream();
        }

        private TweetData BuildTweetFromStream(UrlStreamAttribute urlStreamAttribute, IAsyncCollector<TweetData> asyncCollector)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {bearerToken}");
            var streamAsync = Task.Run(() => _httpClient.GetStreamAsync(urlStreamAttribute.Url)).Result;

            using (var reader = new StreamReader(streamAsync))
            {
                var tweetString = reader.ReadLine();
                while (tweetString != null)
                {
                    tweetString = reader.ReadLine();

                    return Newtonsoft.Json.JsonConvert.DeserializeObject<TweetData>(tweetString);
                }
                return null;
            }
        }
    }
}
