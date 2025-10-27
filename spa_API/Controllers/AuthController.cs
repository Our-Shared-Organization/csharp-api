using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public AuthController(SpasalonContext context) => _context = context;

        [HttpPost("register")]
        public async Task<ActionResult> Register(User user)
        {
            if (await _context.Users.AnyAsync(u => u.Userlogin == user.Userlogin))
                return BadRequest("User already exists");

            user.Userroleid = 1;
            user.Userstatus = true;

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
            if (user == null || user.Userpassword != request.Password)
                return Unauthorized("Invalid credentials");
            if (user.Userstatus == false)
                return Unauthorized("Account deactivated");
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