using JWTAuthentication.Common.Entities;

namespace JWTAuthentication.Common.Interfaces
{
    /// <summary>
    /// IUtilities
    /// </summary>
    public interface IUtilities
    {
        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>string</returns>
        string GenerateJwtToken(User user);

        /// <summary>
        /// Generates the refresh token.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>RefreshToken</returns>
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
