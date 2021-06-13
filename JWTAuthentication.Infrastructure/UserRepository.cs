using JWTAuthentication.Common.Entities;
using JWTAuthentication.Common.Interfaces;
using JWTAuthentication.Common.Models;
using System.Collections.Generic;

namespace JWTAuthentication.Infrastructure
{
    /// <summary>
    /// UserRepository
    /// </summary>
    /// <seealso cref="JWTAuthentication.Common.Interfaces.IUserInfrastructure" />
    public class UserRepository : IUserInfrastructure
    {
        private readonly IUserDatabase _userDatabase;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="userDatabase">The user service.</param>
        public UserRepository(IUserDatabase userDatabase)
        {
            _userDatabase = userDatabase;
        }

        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            return _userDatabase.Authenticate(model, ipAddress);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Collection of User</returns>
        public IEnumerable<User> GetAll()
        {
            return _userDatabase.GetAll();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User</returns>
        public User GetById(int id)
        {
            return _userDatabase.GetById(id);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            return _userDatabase.RefreshToken(token, ipAddress);
        }

        /// <summary>
        /// Revokes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>bool</returns>
        public bool RevokeToken(string token, string ipAddress)
        {
            return _userDatabase.RevokeToken(token, ipAddress);
        }
    }
}
