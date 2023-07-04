using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Albon.IdentityTokenProvider
{
    public interface ITokenValidityService
    {
        public TimeSpan GetValidityPeriod(string accountId);
    }
}
