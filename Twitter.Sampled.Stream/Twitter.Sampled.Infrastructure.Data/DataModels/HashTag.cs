using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Twitter.Sampled.Infrastructure.Data.DataModels
{
    public class HashTag
    {
        public HashTag() => Id = Guid.NewGuid();
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Tag { get; set; }

        public Tweet Tweet { get; set; }
        public ulong TweetId { get; set; }
    }
}
