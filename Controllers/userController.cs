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
        [ProducesResponseType<UserGetResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult Get(int userId)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserId == userId);
            if (foundUser == null) return NotFound(new RequestError { message = "Пользователь с таким ID не найден" });
            
            return Ok(new UserGetResponse { UserId = foundUser.UserId, UserName = foundUser.UserName, UserSurname = foundUser.UserSurname, UserPhone = foundUser.UserPhone, UserSex = foundUser.UserSex, UserRoleId = foundUser.UserRoleId, UserStatus = foundUser.UserStatus });
        }
        
        [HttpPost("register")]
        [ProducesResponseType<UserGetResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult registration([FromBody] UserRegisterRequest userRegisterRequest)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserPhone == userRegisterRequest.UserPhone);
            if (foundUser != null) return BadRequest(new RequestError { message = "Данный номер телефона уже используется. Выполните вход" });
            
            string hashPassword = passwordHasher.HashPassword(userRegisterRequest.UserPhone, userRegisterRequest.UserPassword);

            User newUser = new User()
            {
                UserName = userRegisterRequest.UserName,
                UserSurname = userRegisterRequest.UserSurname,
                UserPhone = userRegisterRequest.UserPhone,
                UserSex = userRegisterRequest.UserSex,
                UserPassword = hashPassword,
                UserRoleId = 1,
            };

            context.Users.Add(newUser);
            context.SaveChanges();
            
            return Ok(new UserRegisterResponse
            {
                UserId = newUser.UserId, UserName = newUser.UserName, UserSurname = newUser.UserSurname,
                UserPhone = newUser.UserPhone, UserSex = newUser.UserSex, UserRoleId = newUser.UserRoleId,
                UserPassword = newUser.UserPassword, UserStatus = newUser.UserStatus
            });
        }

        [HttpPost("auth")]
        [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult Authentication([FromBody] AuthRequest authRequest)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserPhone == authRequest.phone);
            if (foundUser == null) return NotFound(new { message = "Пользователь с такими данными не найден" });
            
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(foundUser.UserPhone, foundUser.UserPassword, authRequest.password);
            if (verificationResult == PasswordVerificationResult.Failed) return BadRequest(new { message = "Неправильный пароль" });
            
            return Ok(new AuthResponse { UserId = foundUser.UserId, UserName = foundUser.UserName, UserSurname = foundUser.UserSurname, UserPhone = foundUser.UserPhone, UserSex = foundUser.UserSex, UserRoleId = foundUser.UserRoleId, UserPassword = foundUser.UserPassword, UserStatus = foundUser.UserStatus });
        }

        [HttpPatch("{userId}/edit")]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult EditUser(int userId, string name, string surname, string phone, string sex)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserPhone == phone);
            if (foundUser != null) return BadRequest(new RequestError { message = "Данный номер телефона уже используется" });

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
