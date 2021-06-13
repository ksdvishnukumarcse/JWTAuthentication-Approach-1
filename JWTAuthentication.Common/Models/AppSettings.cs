namespace JWTAuthentication.Common.Models
{
    /// <summary>
    /// AppSettings
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>
        /// The secret.
        /// </value>
        public string Secret { get; set; }

        /// <summary>
        /// Gets or sets the JWT expires in minutes.
        /// </summary>
        /// <value>
        /// The JWT expires in minutes.
        /// </value>
        public int JwtExpiresInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the refresh expires in minutes.
        /// </summary>
        /// <value>
        /// The refresh expires in minutes.
        /// </value>
        public int RefreshExpiresInMinutes { get; set; }
    }
}
