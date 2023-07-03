using CryptographyTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTokenProvider
{
    public class SessionTokenParameters
    {
        public KeyPair KeyPair { get; set; }

        public DateTime EndValidityDateKeyPair { get; set; }
    }
}
