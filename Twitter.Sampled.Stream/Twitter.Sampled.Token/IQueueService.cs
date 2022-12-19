namespace Twitter.Sampled.Infrastructure.Services
{
    public interface IQueueService
    {
        bool Exists();
        Task SendMessage(string message);
    }
}