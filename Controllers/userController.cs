using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;
using Microsoft.AspNetCore.Identity;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        spaSalonDbContext context = new spaSalonDbContext();
        PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            List<User> foundUser = context.Users.ToList().Where(u => u.UserId == userId).ToList();
            if (foundUser.Count == 0) return BadRequest(new { message = "Пользователь с таким ID не найден" });
            
            return Ok(foundUser.First());
        }
        
        [HttpPost("register")]
        public IActionResult registration(string name, string surname, string phone, string sex, string password)
        {
            foreach (var item in context.Users)
            {
                if (item.UserPhone == phone)
                {
                    return BadRequest(new { message = "Данный номер телефона уже используется. Выполните вход" });
                }
            }
            
            string hashPassword = passwordHasher.HashPassword(phone, password);

            var newUser = new User()
            {
                UserName = name,
                UserSurname = surname,
                UserPhone = phone,
                UserSex = sex,
                UserPassword = hashPassword,
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            return Ok(new { message = "Успешная регистрация!" });
        }

        [HttpPost("auth")]
        public IActionResult Authorizate(string phone, string password)
        {
            List<User> foundClient = context.Users.ToList().Where(u => u.UserPhone == phone).ToList();
            if (foundClient.Count == 0) return BadRequest(new { message = "Пользователь с такими данными не найден" });
            
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(foundClient[0].UserPhone, foundClient[0].UserPassword, password);
            if (verificationResult == PasswordVerificationResult.Failed) return BadRequest(new { message = "Пароли не совпадают" });
            
            return Ok(foundClient.First());
        }

        [HttpPatch("{userId}/edit")]
        public IActionResult EditUser(int userId, string name, string surname, string phone, string sex)
        {
            foreach (var item in context.Users)
            {
                if (item.UserPhone == phone)
                {
                    return BadRequest(new { message = "Данная почта уже используется" });
                }
            }

            var currentClient = context.Users.ToList().Find(a => a.UserId == userId);
            currentClient.UserName = name;
            currentClient.UserSurname = surname;
            currentClient.UserPhone = phone;
            currentClient.UserSex = sex;

            context.SaveChanges();

            return Ok();
        }
    }
}
