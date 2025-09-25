using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        spaSalonDbContext context = new spaSalonDbContext();

        [HttpPost("register")]
        public IActionResult registration(string Email, string password, string name, string surname, string Phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == Email)
                {
                    return BadRequest("Данная почта уже используется, Выполните вход");
                }
            }

            var newUser = new User()
            {
                Email = Email,
                PasswordHash = password,
                FirstName = name,
                LastName = surname,
                PhoneNumber = Phone,
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return Ok("Успешная регистрация!");
        }

        [HttpPost("auth")]
        public IActionResult Authorizate(string email, string password)
        {
            List<User> foundUser = context.Users.ToList().Where(u => u.Email == email && u.PasswordHash == password).ToList();
            if (foundUser.Count == 0) return BadRequest("Пользователь с такими данными не найден");
            Console.WriteLine(foundUser);
            
            return Ok(foundUser);
        }

        [HttpPatch("{userId}/edit")]
        public IActionResult EditUser(int userId, string Email, string password, string name, string surname, string Phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == Email)
                {
                    return BadRequest("Данная почта уже используется");
                }
            }

            var current_User = context.Users.ToList().Find(a => a.UserId == userId);
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
