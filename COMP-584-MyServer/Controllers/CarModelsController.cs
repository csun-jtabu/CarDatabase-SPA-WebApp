using CarWorldModel;
using COMP_584_MyServer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarModelsController : ControllerBase
    {
        private readonly Comp584MyCarDbContext _context;

        public CarModelsController(Comp584MyCarDbContext context)
        {
            _context = context;
        }

        // GET: api/CarModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarModelInfo>>> GetCarModels()
        {
            // return await _context.CarModels.ToListAsync();
            return await _context.CarModels
                .Select(m => new CarModelInfo
                {
                    Id = m.Id,
                    MakeId = m.MakeId,
                    Model = m.Model,
                    Mpg = m.Mpg,
                    Cylinders = m.Cylinders,
                    Displacement = m.Displacement,
                    Horsepower = m.Horsepower,
                    Weight = m.Weight,
                    Acceleration = m.Acceleration,
                    ModelYear = m.ModelYear,
                    Make = m.Make.Make  // Use the navigation property to get the CarMake name
                })
                .ToListAsync();
        }

        // GET: api/CarModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarModel>> GetCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);

            if (carModel == null)
            {
                return NotFound();
            }

            return carModel;
        }

        // PUT: api/CarModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarModel(int id, CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(carModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarModelExists(id))
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

        // POST: api/CarModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModel carModel)
        {
            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);
        }

        // DELETE: api/CarModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }
    }
}
