using Microsoft.AspNetCore.Mvc;
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
            Service? foundService = context.Services.FirstOrDefault(s => s.ServiceName == serviceAddRequest.serviceName);
            if (foundService != null) return BadRequest(new RequestError { message = "Данная услуга уже существует" });

            Service newService = new Service()
            {
                ServiceName = serviceAddRequest.serviceName,
                ServiceDescription = serviceAddRequest.serviceDescription,
                ServiceDuration = serviceAddRequest.serviceDuration, // Считаем в минутах
                ServicePrice = serviceAddRequest.servicePrice,
                ServiceCategoryId = serviceAddRequest.serviceCategoryId,
            };

            context.Services.Add(newService);
            context.SaveChanges();

            return Ok(new ServiceAddResponse { ServiceId = newService.ServiceId, ServiceName = newService.ServiceName, ServiceDescription = newService.ServiceDescription, ServiceDuration = newService.ServiceDuration, ServicePrice = newService.ServicePrice, ServiceCategoryId = newService.ServiceCategoryId, ServiceStatus = newService.ServiceStatus});
        }
    }
}
