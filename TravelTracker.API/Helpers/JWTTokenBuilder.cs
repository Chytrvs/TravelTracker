using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TravelTracker.API.Data;

namespace TravelTracker.API.Helpers
{
    public class JWTTokenBuilder : IJWTTokenBuilder
    {
        private readonly IConfiguration _configuration;
        private string signingKey;
        private int expiryDuration;
        public JWTTokenBuilder(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public string BuildJWTToken(User user)
        {
            SecurityTokenDescriptor tokenDescriptor = CreateSecurityTokenDescriptor(user);
            JwtSecurityTokenHandler jwtTokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(jwtToken);
        }
        private SecurityTokenDescriptor CreateSecurityTokenDescriptor(User user){
            signingKey = _configuration.GetSection("Jwt:SigningSecret").Value;
            expiryDuration = int.Parse(_configuration.GetSection("Jwt:ExpiryDuration").Value);
            return  new SecurityTokenDescriptor
            {
                Issuer = null,
                Audience = null,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username)
                    }),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)), SecurityAlgorithms.HmacSha512Signature)
            };
        }
    }
}