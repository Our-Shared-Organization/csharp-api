using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly spaSalonDbContext _context;
        private readonly PasswordHasher<string> _passwordHasher;

        public AuthController(spaSalonDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<string>();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Userlogin == user.Userlogin))
                return BadRequest("User already exists");

            user.Userroleid = 1;
            user.Userstatus = true;
            user.Userpassword = _passwordHasher.HashPassword(user.Userlogin, user.Userpassword);


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Registered successfully" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            var user = await _context.Users
                .Include(u => u.Userrole)
                .FirstOrDefaultAsync(u => u.Userlogin == request.Login);
            if (user == null) return Unauthorized("Invalid credentials");
            if (user.Userstatus == false) return Unauthorized("Account deactivated");
            PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user.Userlogin, user.Userpassword, request.Password);
            if (verificationResult == PasswordVerificationResult.Failed) return Unauthorized("Invalid password");
            
            return Ok(new
            {
                UserLogin = user.Userlogin,
                UserName = user.Username,
                Role = user.Userrole.Rolename
            });
        }
    }

    public class LoginRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}