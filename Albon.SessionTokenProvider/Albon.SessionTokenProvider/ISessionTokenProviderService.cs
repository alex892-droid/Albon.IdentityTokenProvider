using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SessionTokenProvider
{
    public interface ISessionTokenProviderService
    {
        public string ProvideSessionToken(string userRole);

        public bool ValidateToken(string token);

        public string GetClaim(string token, string claimType);

        public void ConfigureTokenValidityPeriod(TimeSpan validityPeriod);
    }
}
