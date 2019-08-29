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
            if (celestialObj != null)
            {
                celestialObj.Satellites = _context.CelestialObjects.Where(r => r.OrbitedObjectId == id).ToList();
                return Ok(celestialObj);
            }
            return NotFound();
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObj = _context.CelestialObjects.Where(r => r.Name == name).ToList();
            if (celestialObj.Any())
            {
                foreach (var celestial in celestialObj)
                {
                    celestial.Satellites = _context.CelestialObjects.Where(r => r.OrbitedObjectId == celestial.Id).ToList();
                }
                return Ok(celestialObj);
            }
            return NotFound();

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObj = _context.CelestialObjects.ToList();
            foreach (var celestial in celestialObj)
            {
                celestial.Satellites = _context.CelestialObjects.Where(r => r.OrbitedObjectId == celestial.Id).ToList();
            }
            return Ok(celestialObj);
        }
    }
}
