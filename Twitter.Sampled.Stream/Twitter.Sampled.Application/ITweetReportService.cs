namespace Twitter.Sampled.Application
{
    public interface ITweetReportService
    {
        Task TweetSaved(object sender, EventArgs e);
    }
}