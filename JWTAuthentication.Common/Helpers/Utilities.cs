using JWTAuthentication.Common.Entities;
using JWTAuthentication.Common.Interfaces;
using JWTAuthentication.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWTAuthentication.Common.Helpers
{
    /// <summary>
    /// Utilities
    /// </summary>
    public class Utilities : IUtilities
    {
        private readonly AppSettings _appSettings;

        public Utilities()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utilities"/> class.
        /// </summary>
        /// <param name="appSettings">The application settings.</param>
        public Utilities(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Generates the JWT token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>string</returns>
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.JwtExpiresInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates the refresh token.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>RefreshToken</returns>
        public RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddMinutes(_appSettings.RefreshExpiresInMinutes),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                };
            }
        }
    }
}
