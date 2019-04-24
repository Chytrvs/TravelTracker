using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;

//TODO Add action to add flights to a given trip
//TODO Make sure each flight is attached to a user
//TODO Add action to return user trips
//TODO Add some kind of error system to know what went wrong in a repository


namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TripsController:ControllerBase
    {
        private readonly ITripRepository _repository;

        public TripsController(ITripRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> AddFlight(FlightDTO flightDTO){
            Flight flight =await _repository.AddFlight(flightDTO);
            if(flight!=null){
                return Ok(flight);
            }
            return BadRequest("One of the airports doesnt exist!");
        }
        public async Task<IActionResult> AddAirport(Airport airport){
            Airport newairport =await _repository.AddAirport(airport);
            if(newairport!=null){
                return Ok(newairport);
            }
            return BadRequest("Airport already exists!");
        }
    }
}