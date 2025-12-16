using CarWorldModel;
using COMP_584_MyServer.DTOs;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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
        [Authorize(Roles = "administrator")]
        public async Task<ActionResult<CarModel>> PostCarModel(CarModelCreate dto)
        {
            //_context.CarModels.Add(carModel);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetCarModel", new { id = carModel.Id }, carModel);

            bool makeExists = await _context.CarMakes.AnyAsync(m => m.Id == dto.MakeId);

            if (!makeExists)
            {
                return BadRequest("Invalid MakeId.");
            }

            var carModel = new CarModel
            {
                MakeId = dto.MakeId,
                Model = dto.Model,
                Mpg = dto.Mpg,
                Cylinders = dto.Cylinders,
                Displacement = dto.Displacement,
                Horsepower = dto.Horsepower,
                Weight = dto.Weight,
                Acceleration = dto.Acceleration,
                ModelYear = dto.ModelYear
            };

            _context.CarModels.Add(carModel);
            await _context.SaveChangesAsync();

            // this is to get the make name for the response
            var makeName = await _context.CarMakes
                .Where(m => m.Id == carModel.MakeId)
                .Select(m => m.Make)
                .FirstAsync();

            // this is to create the response DTO
            var result = new CarModelInfo
            {
                Id = carModel.Id,
                MakeId = carModel.MakeId,
                Model = carModel.Model,
                Mpg = carModel.Mpg,
                Cylinders = carModel.Cylinders,
                Displacement = carModel.Displacement,
                Horsepower = carModel.Horsepower,
                Weight = carModel.Weight,
                Acceleration = carModel.Acceleration,
                ModelYear = carModel.ModelYear,
                Make = makeName
            };

            return CreatedAtAction("GetCarModel", new { id = carModel.Id }, result);
        }

        // DELETE: api/CarModels/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "administrator")]
        public async Task<IActionResult> DeleteCarModel(int id)
        {
            var carModel = await _context.CarModels.FindAsync(id);
            
            if (carModel == null)
            {
                return NotFound();
            }

            var result = await _context.CarModels
                .Where(m => m.Id == id)
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
                    Make = m.Make.Make
                })
                .SingleOrDefaultAsync();

            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModels.Any(e => e.Id == id);
        }
    }
}
