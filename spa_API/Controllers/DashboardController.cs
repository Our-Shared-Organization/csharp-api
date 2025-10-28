using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        public spaSalonDbContext _context = new();

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