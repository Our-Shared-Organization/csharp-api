    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public UsersController(SpasalonContext context) => _context = context;

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.Userrole)
                .Where(u => u.Userstatus != false)
                .ToListAsync();
        }

        [HttpGet("history/{login}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetUserHistory(string login)
        {
            return await _context.Bookings
                .Include(b => b.Bookingservice)
                .Include(b => b.BookingmasterloginNavigation)
                .Where(b => b.Bookinguserlogin == login && b.Bookingstatus == "completed")
                .OrderByDescending(b => b.Bookingstart)
                .ToListAsync();
        }

        [HttpPut("role/{login}")]
        public async Task<IActionResult> UpdateUserRole(string login, int roleId)
        {
            var user = await _context.Users.FindAsync(login);
            if (user == null) return NotFound();
            user.Userroleid = roleId;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete/{login}")]
        public async Task<IActionResult> DeleteUser(string login)
        {
            var user = await _context.Users.FindAsync(login);
            if (user == null) return NotFound();
            user.Userstatus = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}