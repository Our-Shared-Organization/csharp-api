using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class appointmentController : ControllerBase
    {
        spaSalonDbContext context = new spaSalonDbContext();

        [HttpPost("add")]
        public IActionResult Add_booking(int serviceId, int clientId, DateTime appointmentDate, DateTime endTime, string status)
        {
            var newAppointment = new Appointment()
            {
                ClientId = clientId,
                ServiceId = serviceId,
                AppointmentDate = appointmentDate,
                EndTime = endTime,
                Status = status,
            };

            context.Appointments.Add(newAppointment);
            context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{appointmentId}/edit")]
        public IActionResult Redact_booking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
        {
            var currentAppointment = context.Appointments.ToList().Find(a => a.AppointmentId == appointmentId);
            currentAppointment.ServiceId = serviceId;
            currentAppointment.AppointmentDate = appointmentDate;
            currentAppointment.EndTime = endDate;
            currentAppointment.Status = status;
            currentAppointment.UpdatedAt = DateTime.Now;

            context.SaveChanges();

            return Ok();
        }
    }
}
