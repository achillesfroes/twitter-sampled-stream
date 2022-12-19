using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Twitter.Sampled.Models;

namespace Twitter.Sampled.Client.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<HashTagReport> HashTagsReport { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost:7069/api/twitter-sampled/tags/top/10"),
                    Method = HttpMethod.Get
                };

                try
                {
                    HttpResponseMessage response = await client.SendAsync(request);
                    string? responseString = await response.Content.ReadAsStringAsync();
                    var statusCode = response.StatusCode;
                    if (response.IsSuccessStatusCode)
                    {
                        HashTagsReport = JsonConvert.DeserializeObject<IEnumerable<HashTagReport>>(responseString);
                    }
                    else
                    {
                        ViewData["message"] = "No data to display";
                    }
                }
                catch (Exception)
                {

                    ViewData["message"] = "Server is not ready yet, refresh this page in a few seconds";
                }
            }
        }
    }
}