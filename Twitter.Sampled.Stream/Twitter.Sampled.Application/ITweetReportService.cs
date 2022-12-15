namespace Twitter.Sampled.Application
{
    public interface ITweetReportService
    {
        void TweetSaved(object sender, EventArgs e);
    }
}