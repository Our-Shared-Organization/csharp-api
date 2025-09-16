using Microsoft.AspNetCore.Mvc;
using System.Numerics;
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
    }
}
