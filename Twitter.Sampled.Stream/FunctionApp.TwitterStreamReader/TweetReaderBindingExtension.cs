using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace FunctionApp.TwitterStreamReader
{
    public static class TweetReaderBindingExtension
    {
        public static IWebJobsBuilder AddTweetReaderBinding(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<TweetReaderBinding>();
            return builder;
        }
    }
}
