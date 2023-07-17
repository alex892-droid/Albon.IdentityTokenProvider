namespace Albon.IdentityTokenProvider
{
    public interface ITokenSignatureService
    {
        public string HashAlgorithm { get; }
        public string Sign(string message);
    }
}
