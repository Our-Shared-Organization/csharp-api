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
        public IActionResult addService(string name, string description, int duration, int price, string category)
        {
            foreach (var item in context.Services)
            {
                if (item.Name == name)
                {
                    return BadRequest("Данная услуга уже существует");
                }
            }

            var newService = new Service()
            {
                Name = name,
                Description = description,
                Duration = duration, // Считаем в минутах
                Price = price,
                Category = category,
            };

            context.Services.Add(newService);
            context.SaveChanges();

            return Ok();
        }
    }
}
