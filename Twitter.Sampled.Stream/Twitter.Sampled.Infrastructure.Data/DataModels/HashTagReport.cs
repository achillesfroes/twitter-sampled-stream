﻿using System.ComponentModel.DataAnnotations;

namespace Twitter.Sampled.Infrastructure.Data.DataModels
{
    public class HashTagReport
    {
        [Key]
        public string Tag { get; set; }
        public int TagCount { get; set; }
    }
}
