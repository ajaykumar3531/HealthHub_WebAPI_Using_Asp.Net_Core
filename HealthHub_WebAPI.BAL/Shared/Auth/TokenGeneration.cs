using HealthHub_WebAPI.Domain.DTO.JWT;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.Shared.JWT_Token
{
    public class TokenGeneration : ITokenManager
    {
        private readonly string _key;
        private readonly byte[] _secret;
        private readonly JwtTokenConfig _jwtTokenConfig;

        public TokenGeneration(JwtTokenConfig jwtTokenConfig)
        {
            // Initialize private fields with provided values
            _key = jwtTokenConfig.Secret;
            _jwtTokenConfig = jwtTokenConfig;
            _secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);

        }

        public Task<string> GenerateTokenAsync(SignInRequest request,string UserId)
        {
            // Create a JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();

            // Create a byte array from the secret key
            var tokenKey = Encoding.ASCII.GetBytes(_key);

            // Create a SecurityTokenDescriptor that defines the token's properties
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Add claims such as user's email, first name, and username to the token\
                    new Claim(JwtRegisteredClaimNames.Actort, UserId),
                    new Claim(JwtRegisteredClaimNames.UniqueName,request.UserName),
                    new Claim(JwtRegisteredClaimNames.Actort, request.Password),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Token expiration time (1 hour)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256),
            };

            // Create a token based on the descriptor
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the generated token as a string
            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
