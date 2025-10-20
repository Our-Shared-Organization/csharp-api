using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class bookingController : ControllerBase
    {
        spaSalonDbContext context = new();

        [HttpPost("add")]
        [ProducesResponseType<BookingAddResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult addBooking([FromBody] BookingAddRequest bookingAddRequest)
        {
            User? user = context.Users.FirstOrDefault(u => u.UserLogin == bookingAddRequest.BookingUserLogin);
            if (user == null) return NotFound(new RequestError { message = "Пользователь не найден" });

            Service? service = context.Services.FirstOrDefault(s => s.ServiceId == bookingAddRequest.BookingServiceId);
            if (service == null) return NotFound(new RequestError { message = "Услуга не найдена" });

            Master? master = context.Masters.FirstOrDefault(m => m.MasterId == bookingAddRequest.BookingMasterId);
            if (master == null) return NotFound(new RequestError { message = "Мастер не найден" });
            
            
            
            bool canPerformService = context.MasterServices.Any(ms => ms.MsMasterId == bookingAddRequest.BookingMasterId && ms.MsServiceId == bookingAddRequest.BookingServiceId);
            if (!canPerformService) return BadRequest(new RequestError { message = "Данный мастер не выполняет эту услугу" });

            Booking newBooking = new Booking()
            {
                BookingUserLogin = bookingAddRequest.BookingUserLogin,
                BookingServiceId = bookingAddRequest.BookingServiceId,
                BookingMasterId = bookingAddRequest.BookingMasterId,
                BookingStart = bookingAddRequest.BookingStart,
                BookingFinish = bookingAddRequest.BookingFinish,
                BookingStatus = bookingAddRequest.BookingStatus,
            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();

            return Ok(new BookingAddResponse
            {
                BookingId = newBooking.BookingId,
                BookingUserLogin = newBooking.BookingUserLogin,
                BookingServiceId = newBooking.BookingServiceId,
                BookingMasterId = newBooking.BookingMasterId,
                BookingStart = newBooking.BookingStart,
                BookingFinish = newBooking.BookingFinish,
                BookingStatus = newBooking.BookingStatus
            });
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