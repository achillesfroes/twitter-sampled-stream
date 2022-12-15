using System.Text.RegularExpressions;

namespace Twitter.Sampled.Models
{
    public class Tweet
    {
        public Tweet()
        {
            PromotedMetrics = new PromotedMetrics();
            PublicMetrics = new PublicMetrics();
            Entities = new Entities();
        }

        public string Id { get; set; }
        private string _text;

        public string Text
        {
            get { return Regex.Replace(_text, @"\r\n?|\n", " "); }
            set { _text = value; }
        }

        public Entities Entities { get; set; }
        public string Lang { get; set; }
        public PromotedMetrics PromotedMetrics { get; set; }
        public PublicMetrics PublicMetrics { get; set; }
        public string Source { get; set; }
    }
}
