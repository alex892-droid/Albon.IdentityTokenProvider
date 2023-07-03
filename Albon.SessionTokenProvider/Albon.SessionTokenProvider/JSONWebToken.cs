using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SessionTokenProvider
{
    public class JSONWebToken
    {
        public Header Header { get; set; }

        public Payload Payload { get; set; }

        public string Signature { get; set; }

        public string ToBase64Url(bool withSignature)
        {
            string base64UrlData = string.Empty;

            if(Header != null)
            {
                base64UrlData += Base64UrlEncoder.Encode(JsonConvert.SerializeObject(Header));
            }
            base64UrlData += ".";
            if (Payload != null)
            {
                base64UrlData += Base64UrlEncoder.Encode(JsonConvert.SerializeObject(Payload));
            }

            if (withSignature)
            {
                base64UrlData += "." + Base64UrlEncoder.Encode(Signature);
            }

            return base64UrlData;
        }

        public JSONWebToken()
        {
            Header = new Header();
            Payload = new Payload();
        }

        public JSONWebToken(string token)
        {
            var tokenParam = token.Split('.');

            var header = Base64UrlEncoder.Decode(tokenParam[0]);
            Header = JsonConvert.DeserializeObject<Header>(header);

            var payload = Base64UrlEncoder.Decode(tokenParam[1]);
            Payload = JsonConvert.DeserializeObject<Payload>(payload);
            Signature = Base64UrlEncoder.Decode(tokenParam[2]);
        }
    }

    public class Header
    {
        public string typ { get; set; }

        public string alg { get; set; }

        public Header() 
        {
            typ = "JWT";
            alg = "RSA";
        }
    }

    public class Payload
    {
        public Dictionary<string, string> Claims { get; set; }

        public Payload()
        {
            Claims = new Dictionary<string, string>();
        }
    }
}
