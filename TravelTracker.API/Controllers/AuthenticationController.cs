using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data.Repositories;

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
        
    }
}