using System.Text.Json.Serialization;

namespace lucentrp.Shared.Models.User
{
    /// <summary>
    /// A class to model database users.
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// The user's unique account id.
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// The date and time the user's account was created.
        /// </summary>
        public DateTime AccountCreated { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// Create a user account.
        /// </summary>
        /// <param name="id">The id that uniquely identifies the user.</param>
        /// <param name="accountCreated">The date and time the user's account was created.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        public UserAccount(long id, DateTime accountCreated, string email, string username, string password)
        {
            ID = id;
            AccountCreated = accountCreated;
            Email = email;
            Username = username;
            Password = password;
        }
    }
}
