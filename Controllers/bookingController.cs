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
        public IActionResult Add_booking(int serviceId, int clientId, DateTime appointmentDate, DateTime endTime, string status)
        {
            var newAppointment = new Booking()
            {
                BookingUserId = clientId,
                BookingServiceId = serviceId,
                BookingStart = appointmentDate,
                BookingFinish = endTime,
                BookingStatus = status,
            };

            context.Bookings.Add(newAppointment);
            context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{appointmentId}/edit")]
        public IActionResult Redact_booking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
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
