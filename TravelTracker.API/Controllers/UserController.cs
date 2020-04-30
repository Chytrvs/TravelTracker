using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            User user = await _userRepository.GetUser(username);
            if (user != null)
            {
                var UserToReturn= _mapper.Map<DetailedUserDTO>(user);
                return new JsonResult(UserToReturn);
            }
            return BadRequest("User cannot be found.");

        }

    }
}