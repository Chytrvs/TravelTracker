using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;
using AutoMapper;


namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize]
    public class AirportsController : ControllerBase
    {
        private readonly IFlightRepository _repository;
        private IMapper _mapper { get; }

        public AirportsController(IFlightRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        /// <summary>
        /// Adds new airport
        /// 
        /// Sample request:
        ///     POST
        ///      http://localhost:5000/api/Airports
        ///     {
        ///       "Name": "Philip S W Goldson International Airport",
        ///       "Acronym": "MZBZ",
        ///       "Latitude": "17.5386",
        ///       "Longitude": "-88.3042"
        ///     }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAirport(Airport airport)
        {
            Airport newairport = await _repository.AddAirport(airport);

            if (newairport != null)
            {
                return new JsonResult(newairport);
            }

            return BadRequest("Airport already exists!");
        }
        /// <summary>
        /// Provides all airports in database
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/Airports
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAirports()
        {
            var airports = await _repository.GetAirports();

            if (airports.Count==0)
            {
                return BadRequest("Airports cannot be found.");
            }

            var AirportsToReturn = _mapper.Map<IEnumerable<AirportResponseDTO>>(airports);
            return new JsonResult(AirportsToReturn);

        }
    }
}