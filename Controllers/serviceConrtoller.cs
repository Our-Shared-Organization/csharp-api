using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class serviceController : ControllerBase
    {
        SpaSalonContext context = new SpaSalonContext();

        [HttpPost("add")]
        public IActionResult addService(string name, string description, int duration, int price, int categoryId)
        {
            Service foundService = context.Services.FirstOrDefault(s => s.ServiceName == name);
            if (foundService != null) return BadRequest(new RequestError { message = "Данная услуга уже существует" });

            var newService = new Service()
            {
                ServiceName = name,
                ServiceDescription = description,
                ServiceDuration = duration, // Считаем в минутах
                ServicePrice = price,
                ServiceCategoryId = categoryId,
            };

            context.Services.Add(newService);
            context.SaveChanges();

            return Ok();
        }

        [HttpGet("byMaster/{masterId}")]
        [ProducesResponseType<List<Service>>(StatusCodes.Status200OK)]
        public IActionResult GetServicesByMaster(int masterId)
        {
            var services = context.MasterServices
                .Where(ms => ms.MsMasterId == masterId)
                .Select(ms => ms.MsService)
                .Where(s => s.ServiceStatus == true)
                .ToList();

            return Ok(services);
        }

        [HttpGet("availableForMaster/{masterId}")]
        [ProducesResponseType<List<Service>>(StatusCodes.Status200OK)]
        public IActionResult GetServicesAvailableForMaster(int masterId)
        {
            var masterServices = context.MasterServices
                .Where(ms => ms.MsMasterId == masterId)
                .Select(ms => ms.MsServiceId)
                .ToList();

            var availableServices = context.Services
                .Where(s => s.ServiceStatus == true && !masterServices.Contains(s.ServiceId))
                .ToList();

            return Ok(availableServices);
        }

        [HttpGet("withMasters")]
        [ProducesResponseType<List<Service>>(StatusCodes.Status200OK)]
        public IActionResult GetServicesWithMasters()
        {
            var services = context.Services
                .Where(s => s.ServiceStatus == true)
                .Include(s => s.MasterServices)
                    .ThenInclude(ms => ms.MsMaster)
                        .ThenInclude(m => m.MasterUser)
                .ToList();

            return Ok(services);
        }

        [HttpGet("categories")]
        [ProducesResponseType<List<Category>>(StatusCodes.Status200OK)]
        public IActionResult GetAllCategories()
        {
            var categories = context.Categories
                .Where(c => c.CategoryStatus == true)
                .ToList();

            return Ok(categories);
        }

        [HttpGet("byCategory/{categoryId}")]
        [ProducesResponseType<List<Service>>(StatusCodes.Status200OK)]
        public IActionResult GetServicesByCategory(int categoryId)
        {
            var services = context.Services
                .Where(s => s.ServiceCategoryId == categoryId && s.ServiceStatus == true)
                .ToList();

            return Ok(services);
        }
    }
}