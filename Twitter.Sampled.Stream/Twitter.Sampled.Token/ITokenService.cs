namespace Twitter.Sampled.Token
{
    public interface ITokenService
    {
        Task<Token> GetToken();
    }
}