using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public BookingsController(SpasalonContext context) => _context = context;

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.BookinguserloginNavigation)
                .Include(b => b.Bookingservice)
                .Include(b => b.BookingmasterloginNavigation)
                .OrderBy(b => b.Bookingstart)
                .ToListAsync();
        }

        [HttpGet("user/{userLogin}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingsByUser(string userLogin)
        {
            return await _context.Bookings
                .Include(b => b.Bookingservice)
                .Include(b => b.BookingmasterloginNavigation)
                .Where(b => b.Bookinguserlogin == userLogin)
                .OrderByDescending(b => b.Bookingstart)
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            if (booking.Bookingstart <= DateTime.Now)
                return BadRequest("Booking must be in future");

            // Проверяем существование пользователя
            var user = await _context.Users.FindAsync(booking.Bookinguserlogin);
            if (user == null)
                return BadRequest("User doesn't exist");

            var master = await _context.Users
                .FirstOrDefaultAsync(u => u.Userlogin == booking.Bookingmasterlogin && u.Userroleid == 2);
            if (master == null)
                return BadRequest("Master doesn't exist or is not a master");

            var service = await _context.Services.FindAsync(booking.Bookingserviceid);
            if (service == null)
                return BadRequest("Service doesn't exist");

            var conflict = await _context.Bookings
                .Where(b => b.Bookingmasterlogin == booking.Bookingmasterlogin &&
                           b.Bookingstart < booking.Bookingfinish &&
                           b.Bookingfinish > booking.Bookingstart &&
                           b.Bookingstatus != "cancelled")
                .FirstOrDefaultAsync();

            if (conflict != null)
                return BadRequest("Time slot not available");

            booking.Bookingbookedat = DateTime.Now;
            booking.Bookingstatus = "booked";

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBookingById), new { id = booking.Bookingid }, booking);
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id) 
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            var timeLeft = booking.Bookingstart - DateTime.Now;
            if (timeLeft.TotalHours < 24)
                return BadRequest("Cancel at least 24 hours before");

            booking.Bookingstatus = "cancelled";
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateBooking(int id, Booking updatedBooking)
        {
            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null) return NotFound();

            existingBooking.Bookingstart = updatedBooking.Bookingstart;
            existingBooking.Bookingfinish = updatedBooking.Bookingfinish;
            existingBooking.Bookingstatus = updatedBooking.Bookingstatus;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Booking>> GetBookingById(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.BookinguserloginNavigation)
                .Include(b => b.Bookingservice)
                .Include(b => b.BookingmasterloginNavigation)
                .FirstOrDefaultAsync(b => b.Bookingid == id);
            if (booking == null) return NotFound();
            return booking;
        }
    }
}