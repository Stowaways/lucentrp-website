using JWT.Algorithms;
using JWT.Builder;
using LucentRP.Shared.Models.User;
using System.Security.Cryptography;

namespace LucentRP.Features.Authentication
{
    /// <summary>
    /// A static class containing authentication utilities.
    /// </summary>
    public static class AuthUtilities
    {
        /// <summary>
        /// Hash a password.
        /// </summary>
        /// <param name="rawPassword">The raw password to hash.</param>
        /// <returns>The hashed password.</returns>
        public static string HashPassword(string rawPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(rawPassword);
        }

        /// <summary>
        /// Verify a password.
        /// </summary>
        /// <param name="rawPassword">The raw password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns>If the password is correct or not.</returns>
        public static bool VerifyPassword(string rawPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(rawPassword, hashedPassword);
        }

        /// <summary>
        /// Generate a user token.
        /// </summary>
        /// <param name="userAccount">The user account to generate the token for.</param>
        /// <param name="publicKey">The public key that will be used to generate the token.</param>
        /// <param name="privateKey">The private key that will be used to generate the token.</param>
        /// <returns>The generated user token.</returns>
        public static string CreateUserToken(UserAccount userAccount, RSA publicKey, RSA privateKey)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new RS256Algorithm(publicKey, privateKey))
                .AddClaim("id", userAccount.ID)
                .AddClaim("password", userAccount.Password)
                .Encode();
        }

        /// <summary>
        /// Decode a user token.
        /// </summary>
        /// <param name="token">The token to decode.</param>
        /// <param name="publicKey">The public key that was used to encode the token.</param>
        /// <param name="privateKey">The private key that was used to encode the token.</param>
        /// <returns>The claims associated with the token.</returns>
        public static IDictionary<string, object> DecodeToken(string token, RSA publicKey, RSA privateKey)
        {
            return JwtBuilder.Create()
                .WithAlgorithm(new RS256Algorithm(publicKey, privateKey))
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(token);
        }
        
        /// <summary>
        /// Load an unencrypted key.
        /// </summary>
        /// <param name="file">The file containing the key.</param>
        /// <returns>The key.</returns>
        public static RSA LoadKey(string file)
        {
            RSA key = RSA.Create();
            key.ImportFromPem(File.ReadAllText(file));

            return key;
        }

        /// <summary>
        /// Load an encrypted key.
        /// </summary>
        /// <param name="file">The file containing the key.</param>
        /// <param name="password">The password to decrypt the key.</param>
        /// <returns>The key.</returns>
        public static RSA LoadKey(string file, string password)
        {
            RSA key = RSA.Create();
            key.ImportFromEncryptedPem(file, password);
            return key;
        }
    }
}
