using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        SpaSalonContext context;
        [HttpGet("Registration")]
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

        [HttpPost("Authorization")]
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

        [HttpPatch("Redact_User")]
        public IActionResult Redact_User(int UserId, string Email, string password, string name, string surname, string Phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == Email)
                {
                    return BadRequest("Данная почта уже используется");
                }
            }

            var current_User = context.Users.ToList().Find(a => a.UserId == UserId);
            current_User.Email = Email;
            current_User.PasswordHash = password;
            current_User.FirstName = name;
            current_User.LastName = surname;
            current_User.PhoneNumber = Phone;

            context.SaveChanges();

            return Ok();
        }
    }
}
