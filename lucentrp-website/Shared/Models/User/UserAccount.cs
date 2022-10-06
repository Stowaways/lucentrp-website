namespace LucentRP.Shared.Models.User
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
        public string Password { internal get; set; }

        /// <summary>
        /// If the uuser's email has been verified.
        /// </summary>
        public bool EmailIsVerified { get; set; }

        /// <summary>
        /// If the user requires a password reset.
        /// </summary>
        public bool PasswordResetIsRequired { get; set; }

        /// <summary>
        /// If the user is locked out of their account.
        /// </summary>
        public bool AccountIsLocked { get; set; }

        /// <summary>
        /// If the user's account has been banned.
        /// </summary>
        public bool AccountIsBanned { get; set; }

        /// <summary>
        /// Construct a  new UserAccount.
        /// </summary>
        public UserAccount() 
        {
            ID = -1;
            AccountCreated = DateTime.Now;
            Email = "";
            Username = "";
            Password = "";
        }

        /// <summary>
        /// Construct a  new UserAccount.
        /// </summary>
        /// <param name="id">The id that uniquely identifies the user.</param>
        /// <param name="accountCreated">The date and time the user's account was created.</param>
        /// <param name="email">The user's email address.</param>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        public UserAccount(long id, DateTime accountCreated, string email, string username, string password, bool emailIsVerified, bool passwordResetIsRequired, bool accountIsLocked, bool accountIsBanned)
        {
            ID = id;
            AccountCreated = accountCreated;
            Email = email;
            Username = username;
            Password = password;
            EmailIsVerified = emailIsVerified;
            PasswordResetIsRequired = passwordResetIsRequired;
            AccountIsLocked = accountIsLocked;
            AccountIsBanned = accountIsBanned;
        }
    }
}
