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
            var new_appointment = new Appointment()
            {
                ClientId = clientId,
                ServiceId = serviceId,
                AppointmentDate = appointmentDate,
                EndTime = endTime,
                Status = status,
            };

            context.Appointments.Add(new_appointment);
            context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{appointmentId}/edit")]
        public IActionResult Redact_booking(int appointmentId, int serviceId, DateTime appointmentDate, DateTime endDate, string status)
        {
            var current_Appointment = context.Appointments.ToList().Find(a => a.AppointmentId == appointmentId);
            current_Appointment.ServiceId = serviceId;
            current_Appointment.AppointmentDate = appointmentDate;
            current_Appointment.EndTime = endDate;
            current_Appointment.Status = status;
            current_Appointment.UpdatedAt = DateTime.Now;

            context.SaveChanges();

            return Ok();
        }
    }
}
