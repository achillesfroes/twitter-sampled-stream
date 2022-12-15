using Twitter.Sampled.Models;

namespace Twitter.Sampled.Client.ConsoleApp.Models
{
    public class ReportResponse
    {
        public IEnumerable<HashTagReport> Result { get; set; }
    }
}
