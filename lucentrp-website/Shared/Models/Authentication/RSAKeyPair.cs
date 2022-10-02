using System.Security.Cryptography;

namespace lucentrp.Shared.Models.Authentication
{
    /// <summary>
    /// A class to group RSA keys together.
    /// </summary>
    public class RSAKeyPair
    {
        /// <summary>
        /// The public key.
        /// </summary>
        public RSA PublicKey { get; }

        /// <summary>
        /// The private key.
        /// </summary>
        public RSA PrivateKey { get; }

        /// <summary>
        /// Construct a new RSA key pair.
        /// </summary>
        /// <param name="publicKey">The public key.</param>
        /// <param name="privateKey">The private key.</param>
        public RSAKeyPair(RSA publicKey, RSA privateKey)
        {
            PublicKey = publicKey;
            PrivateKey = privateKey;
        }
    }
}
