using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albon.IdentityTokenProvider
{
    public interface ITokenEncodingService
    {
        public string Base64UrlEncode(string text);
    }
}
