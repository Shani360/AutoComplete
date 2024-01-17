using CitiesApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace CitiesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly DataContext _context;
        public CitiesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetSearchCities")]
        public async Task<ActionResult<List<City>>> GetSearchCities([FromQuery] string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return BadRequest("Query parameter is required.");
            }

            try
            {
                var searchResultCities = _context.Cities.Where(city => city.Name.ToLower().Contains(searchQuery.ToLower())).OrderBy(city => city.Name).Take(1000);
                return Ok(await searchResultCities.ToListAsync());
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }


        }
    }
}
