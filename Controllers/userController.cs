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
        public IActionResult registration(string email, string password, string name, string surname, string phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == email)
                {
                    return BadRequest("Данная почта уже используется, Выполните вход");
                }
            }

            var newUser = new User()
            {
                Email = email,
                PasswordHash = password,
                FirstName = name,
                LastName = surname,
                PhoneNumber = phone,
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
        public IActionResult EditUser(int userId, string email, string password, string name, string surname, string phone)
        {
            foreach (var item in context.Users)
            {
                if (item.Email == email)
                {
                    return BadRequest("Данная почта уже используется");
                }
            }

            var current_User = context.Users.ToList().Find(a => a.UserId == userId);
            current_User.Email = email;
            current_User.PasswordHash = password;
            current_User.FirstName = name;
            current_User.LastName = surname;
            current_User.PhoneNumber = phone;

            context.SaveChanges();

            return Ok();
        }
    }
}
