using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public DashboardController(SpasalonContext context) => _context = context;

        [HttpGet("stats")]
        public async Task<ActionResult<object>> GetStats()
        {
            var totalBookings = await _context.Bookings.CountAsync();
            var todayBookings = await _context.Bookings
                .Where(b => b.Bookingstart.Date == DateTime.Today)
                .CountAsync();
            var totalUsers = await _context.Users.CountAsync();
            var totalServices = await _context.Services.CountAsync();
            return new { totalBookings, todayBookings, totalUsers, totalServices };
        }

        [HttpGet("recent-bookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetRecentBookings()
        {
            return await _context.Bookings
                .Include(b => b.BookinguserloginNavigation)
                .Include(b => b.Bookingservice)
                .OrderByDescending(b => b.Bookingstart)
                .Take(10)
                .ToListAsync();
        }
    }
}