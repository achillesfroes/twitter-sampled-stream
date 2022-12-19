using Twitter.Sampled.Client.ConsoleApp.Models;

ReportResponse? hashTagsReport = null;

using (var client = new HttpClient())
{
    HttpRequestMessage request = new HttpRequestMessage();
    request.RequestUri = new Uri("http://localhost:7069/api/twitter-sampled/tags/top/10");
    request.Method = HttpMethod.Get;
    try
    {
        HttpResponseMessage response = await client.SendAsync(request);
        var responseString = await response.Content.ReadAsStringAsync();
        var statusCode = response.StatusCode;
        if (response.IsSuccessStatusCode)
        {
            hashTagsReport = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportResponse>(responseString);
        }
        else
        {
            Console.WriteLine("No data to display");
        }
    }
    catch (Exception)
    {

        Console.WriteLine("Server is not ready yet");
    }
}

if (hashTagsReport != null)
{
    foreach (var item in hashTagsReport.Result)
    {
        Console.WriteLine($"{item.Tag} : {item.TagCount}");
    }
}


