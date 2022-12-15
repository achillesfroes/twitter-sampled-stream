using Twitter.Sampled.Models;

namespace Twitter.Sampled.Client.WebApp.Models
{
    public class ReportResponse
    {
        public IEnumerable<HashTagReport> Result { get; set; }
    }
}
