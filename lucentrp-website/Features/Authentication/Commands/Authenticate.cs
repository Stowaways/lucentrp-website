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
        /// The query that will be used to get user account information from the database.
        /// </summary>
        private readonly IGetUserAccountByID _getUserAccountByID;

        /// <summary>
        /// Construct an Authenticate command.
        /// </summary>
        /// <param name="rsaKeyPair">The RSA key-pair that will be used for authentication.</param>
        /// <param name="getUserAccountByID">The query that will be used to retrieve user accounts from the database.</param>
        public Authenticate(RSAKeyPair rsaKeyPair, IGetUserAccountByID getUserAccountByID)
        {
            _rsaKeyPair = rsaKeyPair;
            _getUserAccountByID = getUserAccountByID;
        }

        /// <summary>
        /// Authenticate a token.
        /// </summary>
        /// <param name="token">The token to authenticate.</param>
        /// <returns>The user's account if they are authenticated, otherwise null.</returns>
        public UserAccount? Execute(string token)
        {
            IDictionary<string, object> claims = AuthUtilities.DecodeToken(token, _rsaKeyPair.PublicKey, _rsaKeyPair.PrivateKey);

            // If the token does not provide the required claims.
            if (claims["id"] == null || claims["password"] == null)
                return null;

            // If the claims are provided as the wrong datatype.
            if (claims["id"].GetType() != typeof(long) || claims["password"].GetType() != typeof(string))
                return null;

            // Load the user account from the database.
            UserAccount? userAccount = _getUserAccountByID.Execute((long)claims["id"]);

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
