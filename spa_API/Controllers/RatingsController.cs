using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public RatingsController(SpasalonContext context) => _context = context;

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetAllRatings()
        {
            return await _context.Ratings
                .Include(r => r.RatinguserloginNavigation)
                .Include(r => r.RatingmasterloginNavigation)
                .ToListAsync();
        }

        [HttpGet("master/{masterLogin}")]
        public async Task<ActionResult<IEnumerable<Rating>>> GetRatingsByMaster(string masterLogin)
        {
            return await _context.Ratings
                .Include(r => r.RatinguserloginNavigation)
                .Where(r => r.Ratingmasterlogin == masterLogin)
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<Rating>> CreateRating(CreateRatingDto dto)
        {
            if (dto.RatingStars < 1 || dto.RatingStars > 5)
                return BadRequest("Rating must be between 1 and 5");

            if (string.IsNullOrWhiteSpace(dto.RatingText))
                return BadRequest("Rating text cannot be empty");

            var user = await _context.Users.FindAsync(dto.UserLogin);
            if (user == null)
                return BadRequest("User doesn't exist");

            var master = await _context.Users
                .FirstOrDefaultAsync(u => u.Userlogin == dto.MasterLogin && u.Userroleid == 2);
            if (master == null)
                return BadRequest("Master doesn't exist or is not a master");

            var rating = new Rating
            {
                Ratinguserlogin = dto.UserLogin,
                Ratingmasterlogin = dto.MasterLogin,
                Ratingtext = dto.RatingText.Trim(),
                Ratingstars = dto.RatingStars
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRatingById), new { id = rating.Ratingid }, rating);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return NotFound();
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Rating>> GetRatingById(int id)
        {
            var rating = await _context.Ratings
                .Include(r => r.RatinguserloginNavigation)
                .Include(r => r.RatingmasterloginNavigation)
                .FirstOrDefaultAsync(r => r.Ratingid == id);
            if (rating == null) return NotFound();
            return rating;
        }
    }
    public class CreateRatingDto
    {
        public string UserLogin { get; set; } = null!;    // Кто оценивает
        public string MasterLogin { get; set; } = null!;  // Кого оценивают  
        public string RatingText { get; set; } = null!;   // Текст отзыва (обязательный)
        public int RatingStars { get; set; }              // Оценка (1-5)
    }
}