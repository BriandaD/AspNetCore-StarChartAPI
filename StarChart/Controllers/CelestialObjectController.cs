using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObj = _context.CelestialObjects.Find(id);
            celestialObj.Satellites = _context.CelestialObjects.Where(r => r.OrbitedObjectId == id).ToList();

            if (celestialObj != null)
            {
                return Ok();
                
            }
           
            return NotFound();
        }
    }
}
