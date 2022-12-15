using Microsoft.AspNetCore.Mvc.RazorPages;
using Twitter.Sampled.Client.WebApp.Models;

namespace Twitter.Sampled.Client.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ReportResponse HashTagsReport { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
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
                        HashTagsReport = Newtonsoft.Json.JsonConvert.DeserializeObject<ReportResponse>(responseString);
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