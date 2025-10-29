using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;
using whatever_api.Services;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly spaSalonDbContext _context;
        private readonly PasswordHasher<string> _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config;

        public AuthController(spaSalonDbContext context, ITokenService tokenService, IConfiguration config)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<string>();
            _tokenService = tokenService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterRequest user)
        {
            if (await _context.Users.AnyAsync(u => u.Userlogin == user.Userlogin)) return BadRequest(new { message = "User already exists" });

            user.Userroleid = 1;
            user.Userstatus = true;
            user.Userpassword = _passwordHasher.HashPassword(user.Userlogin, user.Userpassword);

            User newUser = new User
            {
                Userlogin = user.Userlogin,
                Userpassword = user.Userpassword,
                Username = user.Username,
                Usersurname = user.Usersurname,
                Userphone = user.Userphone,
                Usersex = user.Usersex,
                Userroleid = user.Userroleid,
                Userstatus = user.Userstatus,
                Useremail = user.Useremail,
            };


            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Userrole)
                .FirstOrDefaultAsync(u => u.Userlogin == request.Login);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });
            if (user.Userstatus == false) return Unauthorized(new { message = "Account deactivated" });
            PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user.Userlogin, user.Userpassword, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed) return Unauthorized(new { message = "Invalid password" });
            
            var token = _tokenService.GenerateToken(user);
            
            return Ok(new
            {
                Token = token,
                UserLogin = user.Userlogin,
                UserName = user.Username,
                Role = user.Userrole.Rolename,
                ExpiresIn = Convert.ToInt32(_config["Jwt:ExpiryInMinutes"]) * 60,
            });
        }
    }

    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}