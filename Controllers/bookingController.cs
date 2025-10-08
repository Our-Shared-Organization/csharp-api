using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class bookingController : ControllerBase
    {
        spaSalonDbContext context = new spaSalonDbContext();

        [HttpPost("add")]
        [ProducesResponseType<BookingAddResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult addBooking([FromBody] BookingAddRequest bookingAddRequest)
        {
            User? user = context.Users.FirstOrDefault(u => u.UserId == bookingAddRequest.BookingUserId);
            if (user == null) return NotFound(new RequestError { message = "Пользователь не найден" });

            Service? service = context.Services.FirstOrDefault(s => s.ServiceId == bookingAddRequest.BookingServiceId);
            if (service == null) return NotFound(new RequestError { message = "Услуга не найдена" });
            
            Booking newBooking = new Booking()
            {
                BookingUserId = bookingAddRequest.BookingUserId,
                BookingServiceId = bookingAddRequest.BookingServiceId,
                BookingStart = bookingAddRequest.BookingStart,
                BookingFinish = bookingAddRequest.BookingFinish,
                BookingStatus = bookingAddRequest.BookingStatus,
            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();

            return Ok(new BookingAddResponse { BookingId = newBooking.BookingId, BookingUserId = newBooking.BookingUserId, BookingServiceId = newBooking.BookingServiceId, BookingStart = newBooking.BookingStart, BookingFinish = newBooking.BookingFinish, BookingStatus = newBooking.BookingStatus });
        }

        [HttpPatch("{appointmentId}/edit")]
        public IActionResult editBooking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
        {
            var currentBooking = context.Bookings.ToList().Find(a => a.BookingId == appointmentId);
            currentBooking.BookingServiceId = serviceId;
            currentBooking.BookingStart = appointmentDate;
            currentBooking.BookingFinish = endDate;
            currentBooking.BookingStatus = status;
            currentBooking.BookingBookedAt = DateTime.Now;

            context.SaveChanges();

            return Ok();
        }
    }
}
