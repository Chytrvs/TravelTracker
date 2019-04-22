using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        public TravelTrackerDbContext _Context { get; set; }

        public ValuesController(TravelTrackerDbContext context)
        {
            _Context = context;
           // _Context.Database.EnsureCreated();
          //  _Context.Points.Add(new PointDataModel{
          //      Latitude=50.076984,
          //      Longitude=19.788205
          //  });
          // _Context.Points.Add(new PointDataModel{
          //     Latitude=5.076984,
          //     Longitude=1.788205
          // });
          //  _Context.SaveChanges();
            
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            //_Context.Points.ToList();
            return Ok(_Context.Users.ToList());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        
        [HttpPost]
        public void Post(double lon)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
