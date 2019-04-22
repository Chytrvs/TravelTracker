using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;

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

        [HttpGet]
        public async Task<IActionResult> ReturnTrips(){
               
            return Ok();
            
        }
        public async Task<IActionResult> AddTrip(Trip trip){
            var response =await _repository.AddTrip(trip);
            if(response!=null){
                return Ok(response);
            }
            return BadRequest("Trip already exists");
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