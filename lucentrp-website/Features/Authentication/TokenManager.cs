using LucentRP.Shared.Models.Authentication;
using LucentRP.Shared.Models.User;

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
        /// <returns>The created token.</returns>
        public string CreateToken(UserAccount userAccount)
        {
            return AuthUtilities.CreateUserToken(userAccount, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);
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
    }
}
