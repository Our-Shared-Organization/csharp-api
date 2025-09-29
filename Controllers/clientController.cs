using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class clientController : ControllerBase
    {
        spaSalonDbContext context = new spaSalonDbContext();

        [HttpGet("{clientId}")]
        public IActionResult Get(int clientId)
        {
            List<Client> foundClient = context.Clients.ToList().Where(u => u.ClientId == clientId).ToList();
            if (foundClient.Count == 0) return BadRequest("Пользователь с таким ID не найден");
            
            return Ok(foundClient.First());
        }
        
        [HttpPost("register")]
        public IActionResult registration(string email, string password, string name, string surname, string phone)
        {
            foreach (var item in context.Clients)
            {
                if (item.ClientEmail == email)
                {
                    return BadRequest("Данная почта уже используется, Выполните вход");
                }
            }

            var newUser = new Client()
            {
                ClientEmail = email,
                ClientPassword = password,
                ClientName = name,
                ClientSurname = surname,
                ClientPhone = phone,
            };

            context.Clients.Add(newUser);
            context.SaveChanges();

            return Ok("Успешная регистрация!");
        }

        [HttpPost("auth")]
        public IActionResult Authorizate(string email, string password)
        {
            List<Client> foundClient = context.Clients.ToList().Where(u => u.ClientEmail == email && u.ClientPassword == password).ToList();
            if (foundClient.Count == 0) return BadRequest("Пользователь с такими данными не найден");
            
            return Ok(foundClient.First());
        }

        [HttpPatch("{clientId}/edit")]
        public IActionResult EditUser(int clientId, string email, string password, string name, string surname, string phone)
        {
            foreach (var item in context.Clients)
            {
                if (item.ClientEmail == email)
                {
                    return BadRequest("Данная почта уже используется");
                }
            }

            var currentClient = context.Clients.ToList().Find(a => a.ClientId == clientId);
            currentClient.ClientEmail = email;
            currentClient.ClientPassword = password;
            currentClient.ClientName = name;
            currentClient.ClientSurname = surname;
            currentClient.ClientPhone = phone;

            context.SaveChanges();

            return Ok();
        }
    }
}
