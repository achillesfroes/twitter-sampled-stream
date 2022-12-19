using System.ComponentModel.DataAnnotations;

namespace Twitter.Sampled.Infrastructure.Data.DataModels
{
    public class HashTagReport : IComparable<HashTagReport>
    {
        [Key]
        public string Tag { get; set; }
        public int TagCount { get; set; }

        public int CompareTo(HashTagReport? other)
        {
            if (other == null) return -1;

            return other.Tag.CompareTo(Tag);
        }
    }
}
