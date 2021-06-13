using JWTAuthentication.Common.Entities;
using JWTAuthentication.Common.Models;
using System.Collections.Generic;

namespace JWTAuthentication.Common.Interfaces
{
    /// <summary>
    /// IUserInfrastructure
    /// </summary>
    public interface IUserInfrastructure
    {
        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        AuthenticateResponse RefreshToken(string token, string ipAddress);

        /// <summary>
        /// Revokes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>bool/returns>
        bool RevokeToken(string token, string ipAddress);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Collection of User</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User</returns>
        User GetById(int id);
    }
}
