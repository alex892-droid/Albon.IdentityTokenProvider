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
