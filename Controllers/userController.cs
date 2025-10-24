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
            User foundUser = context.Users.FirstOrDefault(u => u.Userlogin == userLogin);
            if (foundUser == null) return NotFound(new RequestError { message = "Пользователь с таким ID не найден" });
            
            return Ok(new UserGetResponse { UserLogin = foundUser.Userlogin, UserName = foundUser.Username, UserSurname = foundUser.Usersurname, UserPhone = foundUser.Userphone, UserSex = foundUser.Usersex, UserRoleId = foundUser.Userroleid, UserStatus = foundUser.Userstatus });
        }
        
        [HttpPost("register")]
        [ProducesResponseType<UserGetResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult registration([FromBody] UserRegisterRequest userRegisterRequest)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.Userlogin == userRegisterRequest.UserLogin);
            if (foundUser != null) return BadRequest(new RequestError { message = "Данный логин уже используется. Выполните вход" });
            
            string hashPassword = passwordHasher.HashPassword(userRegisterRequest.UserLogin, userRegisterRequest.UserPassword);

            User newUser = new User()
            {
                Userlogin = userRegisterRequest.UserLogin,
                Username = userRegisterRequest.UserName,
                Usersurname = userRegisterRequest.UserSurname,
                Userphone = userRegisterRequest.UserPhone,
                Usersex = (Usersex)Enum.Parse(typeof(Usersex), userRegisterRequest.UserSex),
                Userpassword = hashPassword,
                Userroleid = 1,
            };

            context.Users.Add(newUser);
            context.SaveChanges();
            
            return Ok(new UserRegisterResponse
            {
                UserLogin = newUser.Userlogin, UserName = newUser.Username, UserSurname = newUser.Usersurname,
                UserPhone = newUser.Userphone, UserSex = newUser.Usersex, UserRoleId = newUser.Userroleid,
                UserPassword = newUser.Userpassword, UserStatus = newUser.Userstatus
            });
        }

        [HttpPost("auth")]
        [ProducesResponseType<UserAuthResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult Authentication([FromBody] UserAuthRequest userAuthRequest)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.Userlogin == userAuthRequest.login);
            if (foundUser == null) return NotFound(new { message = "Пользователь с такими данными не найден" });
            
            PasswordVerificationResult verificationResult = passwordHasher.VerifyHashedPassword(foundUser.Userlogin, foundUser.Userpassword, userAuthRequest.password);
            if (verificationResult == PasswordVerificationResult.Failed) return BadRequest(new { message = "Неправильный пароль" });
            
            return Ok(new UserAuthResponse { UserLogin = foundUser.Userlogin, UserName = foundUser.Username, UserSurname = foundUser.Usersurname, UserPhone = foundUser.Userphone, UserSex = foundUser.Usersex, UserRoleId = foundUser.Userroleid, UserPassword = foundUser.Userpassword, UserStatus = foundUser.Userstatus });
        }

        [HttpPatch("{userLogin}/edit")]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult EditUser(string userLogin, string name, string surname, string phone, string sex)
        {
            User foundUser = context.Users.FirstOrDefault(u => u.Userlogin == userLogin);
            if (foundUser != null) return BadRequest(new RequestError { message = "Данный номер телефона уже используется" });

            var currentClient = context.Users.ToList().Find(a => a.Userlogin == userLogin);
            currentClient.Username = name;
            currentClient.Usersurname = surname;
            currentClient.Userphone = phone;
            currentClient.Usersex = (Usersex)Enum.Parse(typeof(Usersex), sex);

            context.SaveChanges();

            return Ok();
        }
    }
}
