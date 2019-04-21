using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data.Repositories;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    
    public class AuthenticationController:ControllerBase
    {
        private readonly IAuthenticationRepository repository;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IAuthenticationRepository repository,IConfiguration Configuration)
        {
            this.repository = repository;
            _configuration = Configuration;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO RegisterUserDTO){
            User user = await repository.RegisterUser(RegisterUserDTO.Username,RegisterUserDTO.Password);
            if(user==null){
                return BadRequest("User already exists");
            }
            return StatusCode(203);
            
        }
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserDTO LoginUserDTO){
            User user = await repository.LoginUser(LoginUserDTO.Username,LoginUserDTO.Password);
            if(user==null){
                return Unauthorized();
            }

                //var signingKey = Convert.FromBase64String(_configuration["Jwt:SigningSecret"]);
                var signingKey = _configuration.GetSection("Jwt:SigningSecret").Value;
                var expiryDuration = int.Parse(_configuration.GetSection("Jwt:ExpiryDuration").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
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

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return Ok(new{token});
            
            
        }
        
    }
}