using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Twitter.Sampled.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfigurationRoot configurationRoot;
        private readonly Uri authenticationURL;

        public TokenService(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot;

            authenticationURL = new Uri(configurationRoot["Authentication:URL"]);
        }

        public async Task<Token> GetToken()
        {
            HttpClient client = new HttpClient();

            FormUrlEncodedContent content = new FormUrlEncodedContent(GetTokenCredentials());

            HttpResponseMessage response = await client.PostAsync(authenticationURL, content);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                throw new ApplicationException(message);
            }

            string responseString = await response.Content.ReadAsStringAsync();

            Token token = JsonConvert.DeserializeObject<Token>(responseString);

            return token;
        }

        private Dictionary<string, string> GetTokenCredentials()
        {
            return configurationRoot.GetSection("Authentication:Credentials")
                .GetChildren()
                .Select(x => new KeyValuePair<string, string>(x.Key, x.Value))
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}