using System.ComponentModel.DataAnnotations;

namespace JWTAuthentication.Common.Models
{
    /// <summary>
    /// AuthenticateRequest
    /// </summary>
    public class AuthenticateRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        [Required]        
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        public string Password { get; set; }
    }
}
