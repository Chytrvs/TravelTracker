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
using AutoMapper;

namespace TravelTracker.API.Controllers
{   

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository repository;
        private readonly IConfiguration _configuration;

        private IMapper _Mapper { get; }

        public AuthenticationController(IAuthenticationRepository repository, IConfiguration Configuration, IMapper mapper)
        {
            this.repository = repository;
            _configuration = Configuration;
            _Mapper = mapper;
        }
        /// <summary>
        /// Registers user provided in RegisterUserDTO
        /// </summary>
        /// <returns>
        /// Returns HTTP status codes, if user was registered properly, it returns 200 OK, otherwise it returns BadRequest
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO RegisterUserDTO)
        {
            User user = await repository.RegisterUser(RegisterUserDTO.Username, RegisterUserDTO.Password, RegisterUserDTO.Email);
            if (user == null)
            {
                return BadRequest("User already exists");
            }
            var userForReturn = _Mapper.Map<DetailedUserDTO>(user);
            return Ok(userForReturn);

        }
        /// <summary>
        /// Logs in user provided in LoginUserDTO
        /// </summary>
        /// <returns>
        /// If user logs in properly, it returns generated JWT token, otherwise, it returns 401 Unauthorized
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserDTO LoginUserDTO)
        {
            //Checks if user exists
            User user = await repository.LoginUser(LoginUserDTO.Username, LoginUserDTO.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            //Creates token descriptor based on data provided by JWT configuration file and database response
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
            //Generates JWT token based on token descriptor
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return Ok(new { token });


        }

    }
}