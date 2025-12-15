using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarWorldModel;
using COMP_584_MyServer.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMakesController(Comp584MyCarDbContext context) : ControllerBase
    {
        // GET: api/CarMakes
        [HttpGet]
        [Authorize(Roles = "registereduser, administrator")]
        public async Task<ActionResult<IEnumerable<CarMake>>> GetCarMakes()
        {
            return await context.CarMakes.ToListAsync();
        }

        // GET: api/CarMakes/ModelCount
        [HttpGet("modelcount")]
        public async Task<ActionResult<IEnumerable<CarMakesModelCount>>> GetCarMakesModelCount()
        {
            return await context.CarMakes
                .Select(c => new CarMakesModelCount
                {
                    Id = c.Id,
                    Make = c.Make,
                    Origin = c.Origin,
                    ModelCount = c.CarModels.Count()
                })
                .ToListAsync();
        }

        // GET: api/CarMakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarMake>> GetCarMake(int id)
        {
            var carMake = await context.CarMakes.FindAsync(id);

            if (carMake == null)
            {
                return NotFound();
            }

            return carMake;
        }

        // GET: api/CarMakes/ModelCount/5
        [HttpGet("modelcount/{id}")]
        public ActionResult<CarMakesModelCount> GetCarMakesModelCount(int id)
        {
            return context.CarMakes
                .Select(c => new CarMakesModelCount
                {
                    Id = c.Id,
                    Make = c.Make,
                    Origin = c.Origin,
                    ModelCount = c.CarModels.Count()
                })
                .Single(c => c.Id == id);
        }

        // PUT: api/CarMakes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarMake(int id, CarMake carMake)
        {
            if (id != carMake.Id)
            {
                return BadRequest();
            }

            context.Entry(carMake).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarMakeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarMakes
        // This adds a new Car Make to the database. Only users in the "administrator" role can perform this action.
        [HttpPost]
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<CarMake>> PostCarMake(CarMakeCreate dto)
        {
            var carMake = new CarMake
            {
                Make = dto.Make,
                Origin = dto.Origin
            };
            context.CarMakes.Add(carMake);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCarMake", new { id = carMake.Id }, carMake);
        }

        // DELETE: api/CarMakes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarMake(int id)
        {
            var carMake = await context.CarMakes.FindAsync(id);
            if (carMake == null)
            {
                return NotFound();
            }

            context.CarMakes.Remove(carMake);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarMakeExists(int id)
        {
            return context.CarMakes.Any(e => e.Id == id);
        }
    }
}
