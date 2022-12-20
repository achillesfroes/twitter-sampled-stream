namespace Twitter.Sampled.Models
{
    public class Entities
    {
        public Entities()
        {
            Hashtags = new List<Hashtag>();
            Urls = new List<Urls>();
        }

        public List<Hashtag> Hashtags { get; set; }

        public List<Urls> Urls { get; set; }
    }
}
