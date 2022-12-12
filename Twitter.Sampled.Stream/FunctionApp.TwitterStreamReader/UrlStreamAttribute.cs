using Microsoft.Azure.WebJobs.Description;
using System;

namespace FunctionApp.TwitterStreamReader
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class UrlStreamAttribute : Attribute
    {
        public string Url { get; set; }
    }
}
