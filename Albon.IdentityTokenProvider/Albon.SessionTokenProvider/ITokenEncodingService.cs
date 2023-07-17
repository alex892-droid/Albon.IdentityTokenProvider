namespace Albon.IdentityTokenProvider
{
    public interface ITokenEncodingService
    {
        public string Base64UrlEncode(string text);
    }
}
