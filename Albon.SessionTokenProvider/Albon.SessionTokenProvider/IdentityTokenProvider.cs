using Albon.IdentityTokenProvider;
using TechnicalToolsSharedKernel;

namespace SessionTokenProvider
{
    public class IdentityTokenProvider
    {
        public ITokenSignatureService TokenSignatureService { get; set; }

        public ITokenValidityService TokenValidityService { get; set; }

        public ITokenEncodingService TokenEncodingService { get; set; }

        private string Header;

        public IdentityTokenProvider(ITokenSignatureService tokenSignatureService)
        : this(tokenSignatureService, Singleton<DefaultValidityService>.Value, Singleton<DefaultTokenEncodingService>.Value)
        {
        }

        public IdentityTokenProvider(ITokenSignatureService tokenSignatureService, ITokenEncodingService tokenEncodingService)
        : this(tokenSignatureService, Singleton<DefaultValidityService>.Value, tokenEncodingService)
        {
        }

        public IdentityTokenProvider(ITokenSignatureService tokenSignatureService, ITokenValidityService tokenValidityService)
        : this(tokenSignatureService, tokenValidityService, Singleton<DefaultTokenEncodingService>.Value)
        {
        }

        public IdentityTokenProvider(ITokenSignatureService tokenSignatureService, ITokenValidityService tokenValidityService, ITokenEncodingService tokenEncodingService)
        {
            TokenSignatureService = tokenSignatureService;
            TokenValidityService = tokenValidityService;
            TokenEncodingService = tokenEncodingService;
            Header = TokenEncodingService.Base64UrlEncode($"{{\"typ\":\"JWT\", \"alg\":\"{TokenSignatureService.HashAlgorithm}\"}}");
        }

        public string ProvideIdentityToken(string identity)
        {
            string payload = TokenEncodingService.Base64UrlEncode($"{{\"id\":\"{identity}\",\"iat\":{DateTime.Now.Ticks},\"exp\":{(DateTime.Now + TokenValidityService.GetValidityPeriod(identity)).Date.Ticks}}}");
            string signature = TokenEncodingService.Base64UrlEncode(TokenSignatureService.Sign($"{Header}.{payload}"));
            return $"{Header}.{payload}.{signature}";
        }
    }
}
