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

            Booking newBooking = new Booking()
            {
                BookingUserLogin = bookingAddRequest.BookingUserLogin,
                BookingServiceId = bookingAddRequest.BookingServiceId,
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

        // [HttpPost("addWithMaster")]
        // [ProducesResponseType<BookingAddResponse>(StatusCodes.Status200OK)]
        // [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        // public IActionResult addBookingWithMaster([FromBody] BookingAddWithMasterRequest request)
        // {
        //     User? user = context.Users.FirstOrDefault(u => u.UserLogin == request.BookingUserLogin);
        //     if (user == null) return NotFound(new RequestError { message = "Пользователь не найден" });
        //
        //     Service? service = context.Services.FirstOrDefault(s => s.ServiceId == request.BookingServiceId);
        //     if (service == null) return NotFound(new RequestError { message = "Услуга не найдена" });
        //
        //     Master? master = context.Masters.FirstOrDefault(m => m.MasterId == request.BookingMasterId);
        //     if (master == null) return NotFound(new RequestError { message = "Мастер не найден" });
        //
        //     // Проверка, может ли мастер выполнять эту услугу
        //     bool canPerformService = context.MasterServices
        //         .Any(ms => ms.MsMasterId == request.BookingMasterId && ms.MsServiceId == request.BookingServiceId);
        //
        //     if (!canPerformService)
        //         return BadRequest(new RequestError { message = "Данный мастер не выполняет эту услугу" });
        //
        //     Booking newBooking = new Booking()
        //     {
        //         BookingUserLogin = request.BookingUserLogin,
        //         BookingServiceId = request.BookingServiceId,
        //         BookingMasterId = request.BookingMasterId,
        //         BookingStart = request.BookingStart,
        //         BookingFinish = request.BookingFinish,
        //         BookingStatus = request.BookingStatus,
        //     };
        //
        //     context.Bookings.Add(newBooking);
        //     context.SaveChanges();
        //
        //     return Ok(new BookingAddResponse
        //     {
        //         BookingId = newBooking.BookingId,
        //         BookingUserLogin = newBooking.BookingUserLogin,
        //         BookingServiceId = newBooking.BookingServiceId,
        //         BookingMasterId = newBooking.BookingMasterId,
        //         BookingStart = newBooking.BookingStart,
        //         BookingFinish = newBooking.BookingFinish,
        //         BookingStatus = newBooking.BookingStatus
        //     });
        // }
        //
        // [HttpGet("byMaster/{masterId}")]
        // [ProducesResponseType<List<Booking>>(StatusCodes.Status200OK)]
        // public IActionResult GetBookingsByMaster(int masterId)
        // {
        //     var bookings = context.Bookings
        //         .Where(b => b.BookingMasterId == masterId)
        //         .Include(b => b.BookingUser)
        //         .Include(b => b.BookingService)
        //         .ToList();
        //
        //     return Ok(bookings);
        // }

        // [HttpGet("availableMasters/{serviceId}")]
        // [ProducesResponseType<List<Master>>(StatusCodes.Status200OK)]
        // public IActionResult GetAvailableMastersForService(int serviceId, DateTime dateTime)
        // {
        //     var availableMasters = context.MasterServices
        //         .Where(ms => ms.MsServiceId == serviceId)
        //         .Select(ms => ms.MsMaster)
        //         .Where(m => m.MasterStatus == true)
        //         .Where(m => !context.Bookings.Any(b =>
        //             b.BookingMasterId == m.MasterId &&
        //             b.BookingStart <= dateTime &&
        //             b.BookingFinish >= dateTime &&
        //             b.BookingStatus != "canceled"))
        //         .Include(m => m.MasterUser)
        //         .ToList();
        //
        //     return Ok(availableMasters);
        // }
        //
        // [HttpPatch("{appointmentId}/edit")]
        // public IActionResult editBooking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
        // {
        //     var currentBooking = context.Bookings.ToList().Find(a => a.BookingId == appointmentId);
        //     currentBooking.BookingServiceId = serviceId;
        //     currentBooking.BookingStart = appointmentDate;
        //     currentBooking.BookingFinish = endDate;
        //     currentBooking.BookingStatus = status;
        //     currentBooking.BookingBookedAt = DateTime.Now;
        //
        //     context.SaveChanges();
        //
        //     return Ok();
        // }

        // [HttpGet("user/{UserLogin}")]
        // [ProducesResponseType<List<Booking>>(StatusCodes.Status200OK)]
        // public IActionResult GetUserBookings(string UserLogin)
        // {
        //     var bookings = context.Bookings
        //         .Where(b => b.BookingUserLogin == UserLogin)
        //         .Include(b => b.BookingService)
        //         .Include(b => b.BookingMaster)
        //             .ThenInclude(m => m.MasterUser)
        //         .ToList();
        //
        //     return Ok(bookings);
        // }
    }
}