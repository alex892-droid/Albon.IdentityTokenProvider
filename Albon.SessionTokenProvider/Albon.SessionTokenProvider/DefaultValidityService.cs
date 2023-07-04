using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albon.IdentityTokenProvider
{
    internal class DefaultValidityService : ITokenValidityService
    {
        private TimeSpan duration = new TimeSpan(24, 0, 0);

        public TimeSpan GetValidityPeriod(string accountId)
        {
            return duration;
        }
    }
}
