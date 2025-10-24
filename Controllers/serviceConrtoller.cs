using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class serviceController : ControllerBase
    {
        spaSalonDbContext context = new();

        [HttpPost("add")]
        [ProducesResponseType<ServiceAddResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult addService([FromBody] ServiceAddRequest serviceAddRequest)
        {
            Service? foundService = context.Services.FirstOrDefault(s => s.Servicename == serviceAddRequest.serviceName);
            if (foundService != null) return BadRequest(new RequestError { message = "Данная услуга уже существует" });

            Service newService = new Service()
            {
                Servicename = serviceAddRequest.serviceName,
                Servicedescription = serviceAddRequest.serviceDescription,
                Serviceduration = serviceAddRequest.serviceDuration, // Считаем в минутах
                Serviceprice = serviceAddRequest.servicePrice,
                Servicecategoryid = serviceAddRequest.serviceCategoryId,
            };

            context.Services.Add(newService);
            context.SaveChanges();

            return Ok(new ServiceAddResponse { ServiceId = newService.Serviceid, ServiceName = newService.Servicename, ServiceDescription = newService.Servicedescription, ServiceDuration = newService.Serviceduration, ServicePrice = newService.Serviceprice, ServiceCategoryId = newService.Servicecategoryid, ServiceStatus = newService.Servicestatus });
        }
    }
}