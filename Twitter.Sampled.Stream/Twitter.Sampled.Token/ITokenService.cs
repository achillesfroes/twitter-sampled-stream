namespace Twitter.Sampled.Infrastructure.Services
{
    public interface ITokenService
    {
        Task<Token> GetToken();
    }
}