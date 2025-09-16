using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class User_controller : ControllerBase
    {
        SpaSalonContext context;
        [HttpGet("registration")]
        public IActionResult registration(string Email, string password, string name, string surname, string Phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == Email)
                {
                    return BadRequest("Данная почта уже используется, Выполните вход");
                }
            }

            var new_user = new User()
            {
                Email = Email,
                PasswordHash = password,
                FirstName = name,
                LastName = surname,
                PhoneNumber = Phone,
            };

            context.Users.Add(new_user);
            context.SaveChanges();

            return Ok("Успешная регистрация!");
        }

        [HttpPost("Autorization")]
        public IActionResult Authorizate(string email, string password)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == email && item.PasswordHash == password)
                {
                    return Ok("Успешная авторизация");
                }
            }
            return BadRequest("Неправильные данные");
        }
    }
}
