using Newtonsoft.Json;
using System.Text;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Application.Extensions
{
    public static class StringExtensions
    {
        public static TweetData GetDecodedTweet(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            byte[] data = Convert.FromBase64String(value);
            string? decodedTweet = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<TweetData>(decodedTweet);
        }
    }
}
