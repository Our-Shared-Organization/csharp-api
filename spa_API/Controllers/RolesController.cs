using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public RolesController(SpasalonContext context) => _context = context;

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Role>>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}