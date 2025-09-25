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
        public IActionResult Add_booking(int Service_Id, int Client_Id, DateTime AppointmentDate, DateTime End_time, string status)
        {
            var new_appointment = new Appointment()
            {
                ClientId = Client_Id,
                ServiceId = Service_Id,
                AppointmentDate = AppointmentDate,
                EndTime = End_time,
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
