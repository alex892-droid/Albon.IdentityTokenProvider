using System.Text;
namespace Albon.IdentityTokenProvider
{
    internal class DefaultTokenEncodingService : ITokenEncodingService
    {
        static private Encoding encoding = new UTF8Encoding(false);
        private const char BASE64_CHARACTER_62 = '+';
        private const char BASE64_CHARACTER_63 = '/';
        private const char BASE64_URL_CHARACTER_62 = '-';
        private const char BASE64_URL_CHARACTER_63 = '_';
        private const char BASE64_SINGLE_PAD_CHARACTER = '=';

        public string Base64UrlEncode(string text)
        {
            var _bytes = DefaultTokenEncodingService.encoding.GetBytes(text);
            var _encoded = Convert.ToBase64String(_bytes, 0, _bytes.Length);
            _encoded = _encoded.Split(DefaultTokenEncodingService.BASE64_SINGLE_PAD_CHARACTER)[0];
            _encoded = _encoded.Replace(DefaultTokenEncodingService.BASE64_CHARACTER_62, DefaultTokenEncodingService.BASE64_URL_CHARACTER_62);
            _encoded = _encoded.Replace(DefaultTokenEncodingService.BASE64_CHARACTER_63, DefaultTokenEncodingService.BASE64_URL_CHARACTER_63);
            return _encoded;
        }
    }
}
