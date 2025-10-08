using CarWorldModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMP_584_MyServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(Comp584MyCarDbContext context) : ControllerBase
    {
        // POST: api/CarMakes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("CarMakesController")]
        public async Task<ActionResult> PostCarMakes()
        {
            await context.SaveChangesAsync();
            return Ok();
        }

        // POST: api/CarModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost ("CarModels")]
        public async Task<ActionResult> PostCarModels()
        {
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
