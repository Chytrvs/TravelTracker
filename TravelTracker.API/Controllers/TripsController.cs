using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TripsController:ControllerBase
    {   
            [HttpGet]
            public async Task<IActionResult> ReturnTrips(){

            return Ok();
            
        }
    }
}