using Newtonsoft.Json;

namespace Twitter.Sampled.Token
{
    public class Token
    {

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }
}
