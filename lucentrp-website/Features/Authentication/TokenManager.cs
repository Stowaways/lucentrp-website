using LucentRP.Shared.Models.Authentication;
using LucentRP.Shared.Models.User;
using System.Security.Cryptography;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// The class responsible for managing user tokens.
    /// </summary>
    public class TokenManager
    {
        /// <summary>
        /// The RSA key pair that is used to sign and validate tokens.
        /// </summary>
        private readonly RSAKeyPair _rsaKeyPair;

        /// <summary>
        /// Construct a new TokenManager.
        /// </summary>
        /// <param name="rsaKeyPair">The RSA key pair that will be used to sign and validate tokens.</param>
        public TokenManager(RSAKeyPair rsaKeyPair)
        {
            _rsaKeyPair = rsaKeyPair;
        }

        /// <summary>
        /// Create a new token.
        /// </summary>
        /// <param name="userAccount">The user account that will be bound to the token.</param>
        /// <returns>A tuple containing an anti cross-site request forgery token, and the user's 
        /// authentication token.</returns>
        public (string, string) CreateToken(UserAccount userAccount)
        {
            string antiCsrfToken = GenerateAntiCsrfToken();
            return (antiCsrfToken, AuthUtilities.CreateUserToken(userAccount, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey, antiCsrfToken));
        }

        /// <summary>
        /// Get the claims encoded in a token.
        /// </summary>
        /// <param name="token">The token to decode.</param>
        /// <returns>The decoded claims.</returns>
        public IDictionary<string, object> DecodeToken(string token)
        {
            return AuthUtilities.DecodeToken(token, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);
        }

        /// <summary>
        /// Generate a cryptographically string string of random bytes.
        /// </summary>
        /// <returns></returns>
        private static string GenerateAntiCsrfToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
    }
}
