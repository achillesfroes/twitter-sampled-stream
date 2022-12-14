using Newtonsoft.Json;

namespace Twitter.Sampled.Infrastructure.Services
{
    public class Token
    {

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
