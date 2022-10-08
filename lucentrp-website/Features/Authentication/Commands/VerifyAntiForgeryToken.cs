using LucentRP.Shared.Models.Authentication;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// A command to verify an anti-forgery token.
    /// </summary>
    public class VerifyAntiForgeryToken : IVerifyAntiForgeryToken
    {
        /// <summary>
        /// The RSA key pair that will be used for validating tokens.
        /// </summary>
        private readonly RSAKeyPair _rsaKeyPair;

        /// <summary>
        /// Construct a VerifyAntiForgeryToken command.
        /// </summary>
        /// <param name="rsaKeyPair"></param>
        public VerifyAntiForgeryToken(RSAKeyPair rsaKeyPair)
        {
            _rsaKeyPair = rsaKeyPair;
        }

        /// <summary>
        /// Verify an anti-forgery token.
        /// </summary>
        /// <param name="token">The authentiation token.</param>
        /// <param name="antiCsrfToken">The expected anti-forgery token.</param>
        /// <returns>If the request has been verified or not.</returns>
        public bool Execute(string token, string antiCsrfToken)
        {
            // Decode the authentication token.
            IDictionary<string, object> claims = AuthUtilities.DecodeToken(token, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);

            // If the authentication token does not an anti csrf token.
            if (claims["CsrfToken"] is not string)
                return false;

            return claims["CsrfToken"].Equals(antiCsrfToken);
        }
    }

    /// <summary>
    /// A command to verify an anti-forgery token.
    /// </summary>
    public interface IVerifyAntiForgeryToken
    {
        /// <summary>
        /// Verify an anti-forgery token.
        /// </summary>
        /// <param name="token">The authentiation token.</param>
        /// <param name="antiCsrfToken">The expected anti-forgery token.</param>
        /// <returns>If the request has been verified or not.</returns>
        bool Execute(string token, string antiCsrfToken);
    }
}
