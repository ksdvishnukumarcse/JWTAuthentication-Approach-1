using JWTAuthentication.Common.Entities;
using JWTAuthentication.Common.Interfaces;
using JWTAuthentication.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWTAuthentication.Infrastructure.InMemory
{
    /// <summary>
    /// /InMemoryUser
    /// </summary>
    /// <seealso cref="JWTAuthentication.Common.Interfaces.IUserDatabase" />
    public class InMemoryUser : IUserDatabase
    {
        private readonly DataContext _context;
        private readonly IUtilities _utilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryUser"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="appSettings">The application settings.</param>
        public InMemoryUser(DataContext context, IUtilities utilities)
        {
            _context = context;
            _utilities = utilities;
        }

        /// <summary>
        /// Authenticates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// AuthenticateResponse
        /// </returns>
        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _utilities.GenerateJwtToken(user);
            var refreshToken = _utilities.GenerateRefreshToken(ipAddress);

            // save refresh token
            user.RefreshTokens.Add(refreshToken);
            _context.Update(user);
            _context.SaveChanges();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>
        /// Collection of User
        /// </returns>
        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// User
        /// </returns>
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        /// <summary>
        /// Refreshes the token.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>
        /// AuthenticateResponse
        /// </returns>
        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return null if no user found with token
            if (user == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!refreshToken.IsActive) return null;

            // replace old refresh token with a new one and save
            var newRefreshToken = _utilities.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = _utilities.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        /// <summary>
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        /// <!-- Badly formed XML comment ignored for member "M:JWTAuthentication.Common.Interfaces.IUserDatabase.RevokeToken(System.String,System.String)" -->
        public bool RevokeToken(string token, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            // return false if no user found with token
            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            _context.Update(user);
            _context.SaveChanges();

            return true;
        }
    }
}
