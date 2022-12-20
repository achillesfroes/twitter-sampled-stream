using Newtonsoft.Json;
using Twitter.Sampled.Models;

Console.OutputEncoding = System.Text.Encoding.Unicode;

var startTimeSpan = TimeSpan.Zero;
var periodTimeSpan = TimeSpan.FromSeconds(2);

var timer = new System.Threading.Timer((e) =>
{
    ReadTweets();
}, null, startTimeSpan, periodTimeSpan);

Console.ReadKey();

async Task ReadTweets()
{
    Console.Clear();

    IEnumerable<HashTagReport> hashTagsReport = null;
    int tweetCount = 0;

    try
    {
        hashTagsReport = await HttpGet<IEnumerable<HashTagReport>>("http://localhost:7069/api/twitter-sampled/tags/top/10");
        tweetCount = await HttpGet<int>("http://localhost:7069/api/twitter-sampled/count");

        if (hashTagsReport != null)
        {
            foreach (var item in hashTagsReport)
            {
                Console.WriteLine($"{item.Tag} : {item.TagCount}");
            }
        }
        else
        {
            Console.WriteLine("No data to display");
        }
        Console.WriteLine("----------------------");
        Console.WriteLine($"Tweets count : {tweetCount}");
        
    }
    catch (Exception)
    {
        Console.WriteLine("Server is not ready yet");
    }
}

static async Task<T> HttpGet<T>(string url)
{
    using (var client = new HttpClient())
    {
        HttpRequestMessage request = new HttpRequestMessage();
        request.RequestUri = new Uri(url);
        request.Method = HttpMethod.Get;

        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();
            var statusCode = response.StatusCode;
            if (response.IsSuccessStatusCode)
            {
                //if (typeof(T) == typeof(IEnumerable<HashTagReport>))
                //{
                    return JsonConvert.DeserializeObject<T>(responseString);
                //}
                //else
                //{
                //    return JsonConvert.DeserializeObject<T>(responseString);
                //}
                
            }
        }
        catch (Exception)
        {

            Console.WriteLine("Server is not ready yet");
        }
    }

    return default(T);
}