using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class serviceController : ControllerBase
    {
        spaSalonDbContext context =  new spaSalonDbContext();

        [HttpPost("add")]
        public IActionResult addService(string name, string description, int duration, int price, int categoryId)
        {
            foreach (var item in context.Services)
            {
                if (item.ServiceName == name)
                {
                    return BadRequest(new { message = "Данная услуга уже существует" });
                }
            }

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
    }
}
