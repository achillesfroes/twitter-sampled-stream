namespace Twitter.Sampled.Infrastructure.Services
{
    public interface ITwitterStreamReader
    {
        Task<StreamReader> GetStreamReader(Token token);
    }
}