using CarWorldModel;
using COMP_584_MyServer.Data;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(Comp584MyCarDbContext context, IHostEnvironment environment) : ControllerBase
    {
        String _pathName = Path.Combine(environment.ContentRootPath, "Data", "Automobile.csv");
        // POST: api/CarMakes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("CarMakesController")]
        public async Task<ActionResult> PostCarMakes()
        {
            // dictionary to hold existing makes
            Dictionary<string, CarMake> carMakes = await context.CarMakes.AsNoTracking().ToDictionaryAsync(
                c => c.Make, StringComparer.OrdinalIgnoreCase);

            // reading from csv file and loading entries into a list
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);
            List<COMP584csv> records = csv.GetRecords<COMP584csv>().ToList();

            // looping through each record from the csv
            foreach (COMP584csv record in records)
            { 
                // check if make already exists
                if (!carMakes.ContainsKey(record.make))
                {
                    // creates a new make entry with the make and origin from the csv record
                    CarMake newMake = new()
                    {
                        Make = record.make,
                        Origin = record.origin
                    };
                    // adds the new make to the dictionary
                    carMakes.Add(record.make, newMake);
                    // adds the new make to the database context
                    await context.CarMakes.AddAsync(newMake);
                }
            }
            await context.SaveChangesAsync();

            // return success message
            return new JsonResult("Successfully added Makes to the Database");
        }

        // POST: api/CarModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("CarModels")]
        public async Task<ActionResult> PostCarModels()
        {
            // dictionary to hold existing makes
            Dictionary<string, CarMake> carMakes = await context.CarMakes.AsNoTracking().ToDictionaryAsync(
                c => c.Make, StringComparer.OrdinalIgnoreCase);

            // reading from csv file and loading entries into a list
            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);
            List<COMP584csv> records = csv.GetRecords<COMP584csv>().ToList();

            // count to keep track of number of models added
            int modelCount = 0;

            // looping through each record from the csv
            foreach (COMP584csv record in records)
            {
                // there are some null values in the csv, so we should skip entries with missing data
                // example being horsepower values that are blank
                if (record.horsepower.HasValue && record.horsepower.Value > 0)
                {
                    CarModel carModel = new()
                    {
                        Model = record.model,
                        Mpg = (double)record.mpg,
                        Cylinders = (int)record.cylinders,
                        Displacement = (double)record.displacement,
                        Horsepower = (int)record.horsepower.Value,
                        Weight = record.weight,
                        Acceleration = (double)record.acceleration,
                        ModelYear = record.model_year,
                        MakeId = carMakes[record.make].Id
                    };
                    await context.CarModels.AddAsync(carModel);
                    modelCount++;
                }
            }
            await context.SaveChangesAsync();

            // output the number of makes added
            return new JsonResult(modelCount);
        }
    }
}
