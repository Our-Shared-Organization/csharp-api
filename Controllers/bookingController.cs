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
            // User? user = context.Users.FirstOrDefault(u => u.Userlogin == bookingAddRequest.BookingUserLogin);
            // if (user == null) return NotFound(new RequestError { message = "Пользователь не найден" });

            Service? service = context.Services.FirstOrDefault(s => s.Serviceid == bookingAddRequest.BookingServiceId);
            if (service == null) return NotFound(new RequestError { message = "Услуга не найдена" });

            User? master = context.Users.FirstOrDefault(m => m.Userlogin == bookingAddRequest.BookingMasterLogin && m.Userroleid == 2);
            if (master == null) return NotFound(new RequestError { message = "Мастер не найден" });
            
            
            
            // bool canPerformService = context.MasterServices.Any(ms => ms.Msmasterid == bookingAddRequest.BookingMasterId && ms.Msserviceid == bookingAddRequest.BookingServiceId);
            // if (!canPerformService) return BadRequest(new RequestError { message = "Данный мастер не выполняет эту услугу" });

            Booking newBooking = new Booking()
            {
                Bookinguserlogin = bookingAddRequest.BookingUserLogin,
                Bookingserviceid = bookingAddRequest.BookingServiceId,
                // Bookingmasterid = bookingAddRequest.BookingMasterId,
                Bookingstart = bookingAddRequest.BookingStart,
                Bookingfinish = bookingAddRequest.BookingFinish,
                Bookingstatus = (Bookingstatus)Enum.Parse(typeof(Bookingstatus),  bookingAddRequest.BookingStatus),
            };

            context.Bookings.Add(newBooking);
            context.SaveChanges();

            return Ok(new BookingAddResponse
            {
                BookingId = newBooking.Bookingid,
                BookingUserLogin = newBooking.Bookinguserlogin,
                BookingServiceId = newBooking.Bookingserviceid,
                BookingMasterLogin = newBooking.Bookingmasterlogin,
                BookingStart = newBooking.Bookingstart,
                BookingFinish = newBooking.Bookingfinish,
                BookingStatus = newBooking.Bookingstatus
            });
        }
        
        [HttpPatch("{appointmentId}/edit")]
        public IActionResult editBooking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
        {
            var currentBooking = context.Bookings.ToList().Find(a => a.Bookingid == appointmentId);
            currentBooking.Bookingserviceid = serviceId;
            currentBooking.Bookingstart = appointmentDate;
            currentBooking.Bookingfinish = endDate;
            currentBooking.Bookingstatus = (Bookingstatus)Enum.Parse(typeof(Bookingstatus), status);
            currentBooking.Bookingbookedat = DateTime.Now;
        
            context.SaveChanges();
        
            return Ok();
        }
    }
}