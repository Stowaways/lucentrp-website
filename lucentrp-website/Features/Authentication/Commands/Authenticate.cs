using LucentRP.Features.Users;
using LucentRP.Shared.Models.Authentication;
using LucentRP.Shared.Models.User;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// A command to authenticate users.
    /// </summary>
    public class Authenticate : IAuthenticate
    {
        /// <summary>
        /// The RSA key pair that will be used for signing and validating tokens.
        /// </summary>
        private readonly RSAKeyPair _rsaKeyPair;

        /// <summary>
        /// The user account manager that will be used to manager user accounts.
        /// </summary>
        private readonly UserAccountManager _userAccountManager;

        /// <summary>
        /// Construct an Authenticate command.
        /// </summary>
        /// <param name="rsaKeyPair">The RSA key-pair that will be used for authentication.</param>
        /// <param name="getUserAccountByID">The user account manager that will be used to manager user accounts.</param>
        public Authenticate(RSAKeyPair rsaKeyPair, UserAccountManager userAccountManager)
        {
            _rsaKeyPair = rsaKeyPair;
            _userAccountManager = userAccountManager;
        }

        /// <summary>
        /// Authenticate a token.
        /// </summary>
        /// <param name="token">The token to authenticate.</param>
        /// <returns>The user's account if they are authenticated, otherwise null.</returns>
        public UserAccount? Execute(string token)
        {
            // Decode the authentication token.
            IDictionary<string, object> claims = AuthUtilities.DecodeToken(token, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);

            // If the token does not provide valid id and password claims.
            if (claims["id"] is not long || claims["password"] is not string)
                return null;

            // Load the user account from the database.
            UserAccount? userAccount = _userAccountManager.GetByID((long)claims["id"]);

            // If the user was not found.
            if (userAccount == null)
                return null;

            // If the passwords do not match.
            if (!userAccount.Password.Equals(claims["password"]))
                return null;

            // The user has been authenticated.
            return userAccount;
        }
    }

    /// <summary>
    /// A command to authenticate users.
    /// </summary>
    public interface IAuthenticate
    {
        /// <summary>
        /// Authenticate a token.
        /// </summary>
        /// <param name="token">The token to authenticate.</param>
        /// <returns>The user's account if they are authenticated, otherwise null.</returns>
        UserAccount? Execute(string token);
    }
}
