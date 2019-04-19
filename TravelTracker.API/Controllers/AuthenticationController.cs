using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data.Repositories;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data;
using System.Threading.Tasks;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController:ControllerBase
    {
        private readonly IAuthenticationRepository repository;

        public AuthenticationController(IAuthenticationRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO RegisterUserDTO){
            User user = await repository.RegisterUser(RegisterUserDTO.Username,RegisterUserDTO.Password);
            if(user==null){
                return BadRequest("User already exists");
            }
            return StatusCode(203);
            
        }
        
    }
}