using BasicLinkedObjectBase;
using Cryptography;
using CryptographyTools;

namespace SessionTokenProvider
{
    public class SessionTokenProviderService : ISessionTokenProviderService
    {
        public ICryptographyService CryptographyService { get; set; }

        public IObjectBaseService ObjectBaseService { get; set; }

        private TimeSpan ValidityPeriod = new TimeSpan(24, 0, 0);

        public static SessionTokenParameters SessionTokenParameters { get; set; }

        public SessionTokenProviderService(ICryptographyService cryptographyService, IObjectBaseService objectBaseService)
        {
            CryptographyService = cryptographyService;
            ObjectBaseService = objectBaseService;
            LoadParameters();
        }

        public string ProvideSessionToken(string userRole)
        {
            JSONWebToken token = new JSONWebToken();
            token.Payload.Claims.Add("UserRole", userRole);
            token.Payload.Claims.Add("ValidityEndDate", (DateTime.Now + ValidityPeriod).ToString());
            var messageToSigne = token.ToBase64Url(false);
            token.Signature = CryptographyService.Sign(messageToSigne, GetKeys().PrivateKey);
            return token.ToBase64Url(true);
        }

        public bool ValidateToken(string token)
        {
            var jsonWebToken = new JSONWebToken(token);
            var message = jsonWebToken.ToBase64Url(false);
            return CryptographyService.VerifySignature(message, jsonWebToken.Signature, GetKeys().PublicKey);
        }

        public string GetClaim(string token, string claimType)
        {
            var jsonWebToken = new JSONWebToken(token);
            return jsonWebToken.Payload.Claims.FirstOrDefault(x => x.Key == claimType).Value;
        }

        public void ConfigureTokenValidityPeriod(TimeSpan validityPeriod)
        {
            ValidityPeriod = validityPeriod;
        }

        private KeyPair GetKeys()
        {
            if (SessionTokenParameters.KeyPair == null || SessionTokenParameters.EndValidityDateKeyPair < DateTime.Now)
            {
                SessionTokenParameters.KeyPair = CryptographyService.GenerateKeyPair();
                SessionTokenParameters.EndValidityDateKeyPair = DateTime.Now.AddDays(1);
                ObjectBaseService.Update(SessionTokenParameters);
            }

            return SessionTokenParameters.KeyPair;
        }

        private void LoadParameters()
        {
            var sessionTokenParameters = ObjectBaseService.Query<SessionTokenParameters>().FirstOrDefault();

            if (sessionTokenParameters == null)
            {
                sessionTokenParameters = new SessionTokenParameters();
                sessionTokenParameters.KeyPair = CryptographyService.GenerateKeyPair();
                sessionTokenParameters.EndValidityDateKeyPair = DateTime.Now.AddDays(1);
                ObjectBaseService.Add(sessionTokenParameters);
            }

            SessionTokenParameters = sessionTokenParameters;
        }
    }
}
