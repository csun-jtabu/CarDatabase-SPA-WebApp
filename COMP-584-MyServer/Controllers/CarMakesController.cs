using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarWorldModel;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarMakesController(Comp584MyCarDbContext context) : ControllerBase
    {
        // GET: api/CarMakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarMake>>> GetCarMakes()
        {
            return await context.CarMakes.ToListAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarMake>> PostCarMake(CarMake carMake)
        {
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
