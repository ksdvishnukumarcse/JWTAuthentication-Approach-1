using JWTAuthentication.Common.Entities;
using JWTAuthentication.Common.Interfaces;
using JWTAuthentication.Common.Models;
using System.Collections.Generic;

namespace JWTAuthentication.Services
{
    /// <summary>
    /// IUserService
    /// </summary>
    /// <seealso cref="JWTAuthentication.Common.Interfaces.IUserService" />
    public class UserService : IUserService
    {
        private readonly IUserInfrastructure _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user Repository.</param>
        public UserService(IUserInfrastructure userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            return _userRepository.Authenticate(model, ipAddress);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>Collection of User</returns>
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>User</returns>
        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>AuthenticateResponse</returns>
        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            return _userRepository.RefreshToken(token, ipAddress);
        }

        /// <summary>
        /// Revokes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>bool</returns>
        public bool RevokeToken(string token, string ipAddress)
        {
            return _userRepository.RevokeToken(token, ipAddress);
        }
    }
}
