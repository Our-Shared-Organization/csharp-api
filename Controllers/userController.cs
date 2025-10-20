using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;
using Microsoft.AspNetCore.Identity;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class userController : ControllerBase
    {
        spaSalonDbContext context = new();
        PasswordHasher<string> passwordHasher = new();

        [HttpGet("{userLogin}")]
        [ProducesResponseType<UserGetResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult Get(string userLogin)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserLogin == userLogin);
            if (foundUser == null) return NotFound(new RequestError { message = "Пользователь с таким ID не найден" });
            
            return Ok(new UserGetResponse { UserLogin = foundUser.UserLogin, UserName = foundUser.UserName, UserSurname = foundUser.UserSurname, UserPhone = foundUser.UserPhone, UserSex = foundUser.UserSex, UserRoleId = foundUser.UserRoleId, UserStatus = foundUser.UserStatus });
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
                UserLogin = newUser.UserLogin, UserName = newUser.UserName, UserSurname = newUser.UserSurname,
                UserPhone = newUser.UserPhone, UserSex = newUser.UserSex, UserRoleId = newUser.UserRoleId,
                UserPassword = newUser.UserPassword, UserStatus = newUser.UserStatus
            });
        }

        [HttpPost("auth")]
        [ProducesResponseType<UserAuthResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult Authentication([FromBody] UserAuthRequest userAuthRequest)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserPhone == userAuthRequest.login);
            if (foundUser == null) return NotFound(new { message = "Пользователь с такими данными не найден" });
            
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(foundUser.UserPhone, foundUser.UserPassword, userAuthRequest.password);
            if (verificationResult == PasswordVerificationResult.Failed) return BadRequest(new { message = "Неправильный пароль" });
            
            return Ok(new UserAuthResponse { UserLogin = foundUser.UserLogin, UserName = foundUser.UserName, UserSurname = foundUser.UserSurname, UserPhone = foundUser.UserPhone, UserSex = foundUser.UserSex, UserRoleId = foundUser.UserRoleId, UserPassword = foundUser.UserPassword, UserStatus = foundUser.UserStatus });
        }

        [HttpPatch("{userLogin}/edit")]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult EditUser(string userLogin, string name, string surname, string phone, string sex)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.UserPhone == phone);
            if (foundUser != null) return BadRequest(new RequestError { message = "Данный номер телефона уже используется" });

            var currentClient = context.Users.ToList().Find(a => a.UserLogin == userLogin);
            currentClient.UserName = name;
            currentClient.UserSurname = surname;
            currentClient.UserPhone = phone;
            currentClient.UserSex = sex;

            context.SaveChanges();

            return Ok();
        }
    }
}
