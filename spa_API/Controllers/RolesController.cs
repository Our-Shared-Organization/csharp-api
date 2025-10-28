using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        public spaSalonDbContext _context = new();

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}