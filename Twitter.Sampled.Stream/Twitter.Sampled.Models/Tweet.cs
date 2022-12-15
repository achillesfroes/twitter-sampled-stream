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
        public string Text { get; set; }
        public Entities Entities { get; set; }
        public string Lang { get; set; }
        public PromotedMetrics PromotedMetrics { get; set; }
        public PublicMetrics PublicMetrics { get; set; }
        public string Source { get; set; }
    }
}
