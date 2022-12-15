using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Sampled.Application
{
    public class TweetReportService : ITweetReportService
    {
        public TweetReportService() { }

        public void TweetSaved(object sender, EventArgs e)
        {
            Console.WriteLine("Evento");
        }
    }
}
