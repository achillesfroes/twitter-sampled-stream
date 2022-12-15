using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Sampled.Infrastructure.Data.DataModels
{
    public class HashTagReport
    {
        [Key]
        public string Tag { get; set; }
        public int TagCount { get; set; }
    }
}
