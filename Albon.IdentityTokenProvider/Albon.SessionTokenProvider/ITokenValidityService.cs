namespace Albon.IdentityTokenProvider
{
    public interface ITokenValidityService
    {
        public TimeSpan GetValidityPeriod(string accountId);
    }
}
