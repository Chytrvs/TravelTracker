using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data.Repositories;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data;
using System.Threading.Tasks;
using AutoMapper;
using TravelTracker.API.Helpers;

namespace TravelTracker.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly IMapper _Mapper;
        private readonly IJWTTokenBuilder _jWTTokenBuilder;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IAuthenticationRepository authRepository, IUserRepository userRepository, IMapper mapper, IJWTTokenBuilder JWTTokenBuilder)
        {
            _userRepository = userRepository;
            _authRepository=authRepository;
            _Mapper = mapper;
            _jWTTokenBuilder = JWTTokenBuilder;
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
            if(await _userRepository.DoesUserExist(RegisterUserDTO.Username))
                return BadRequest("Username is already taken");
            if(await _userRepository.IsEmailTaken(RegisterUserDTO.Email))
                return BadRequest("Email address is already taken");
            User user = await _authRepository.RegisterUser(RegisterUserDTO);
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
            if(!await _userRepository.DoesUserExist(LoginUserDTO.Username))
                return Unauthorized();
            User user = await _authRepository.LoginUser(LoginUserDTO.Username, LoginUserDTO.Password);
            if(user == null)
                return Unauthorized();
            string token=_jWTTokenBuilder.BuildJWTToken(user);
            return Ok(new { token });
        }

    }
}