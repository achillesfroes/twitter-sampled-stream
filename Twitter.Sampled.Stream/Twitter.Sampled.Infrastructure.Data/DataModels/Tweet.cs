﻿namespace Twitter.Sampled.Infrastructure.Data.DataModels
{
    public class Tweet
    {
        public Tweet()
        {
            Tags = new List<HashTag>();
        }

        public ulong Id { get; set; }
        public string Text { get; set; }
        public int RetweetCount { get; set; }
        public int ImpressionCount { get; set; }
        public List<HashTag> Tags { get; set; }
    }
}
