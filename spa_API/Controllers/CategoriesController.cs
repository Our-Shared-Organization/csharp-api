using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        public spaSalonDbContext _context = new();

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories()
        {
            return await _context.Categories
                .Where(c => c.Categorystatus != false)
                .ToListAsync();
        }

        [HttpGet("services/{id}")]
        public async Task<ActionResult<IEnumerable<Service>>> GetServicesByCategory(int id)
        {
            return await _context.Services
                .Include(s => s.Servicecategory)
                .Where(s => s.Servicecategoryid == id)
                .ToListAsync();
        }
    }
}