using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Xml.Linq;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Services_conrtoller : ControllerBase
    {
        SpaSalonContext context;

        [HttpPost("Add_Service")]
        public IActionResult Add_services(string name, string description, int Duration, int Price, string category)
        {
            foreach (var item in context.Services)
            {
                if (item.Name == name)
                {
                    return BadRequest("Данная услуга уже существует");
                }
            }

            var new_service = new Service()
            {
                Name = name,
                Description = description,
                Duration = Duration, // Считаем в минутах
                Price = Price,
                Category = category,
            };

            context.Services.Add(new_service);
            context.SaveChanges();

            return Ok();
        }

        [HttpPost("Add_booking")]
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

        [HttpPatch("Redact_booking")]
        public IActionResult Redact_booking(int AppointmentId, int n_Service_id, DateTime n_AppointmentDate, DateTime n_EndDate, string n_status)
        {
            var current_Appointment = context.Appointments.ToList().Find(a => a.AppointmentId == AppointmentId);
            current_Appointment.ServiceId = n_Service_id;
            current_Appointment.AppointmentDate = n_AppointmentDate;
            current_Appointment.EndTime = n_EndDate;
            current_Appointment.Status = n_status;
            current_Appointment.UpdatedAt = DateTime.Now;

            return Ok();
        }
    }
}
