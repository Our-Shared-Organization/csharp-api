using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spa_API.Model;

namespace spa_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterCategoriesController : ControllerBase
    {
        private readonly SpasalonContext _context;
        public MasterCategoriesController(SpasalonContext context) => _context = context;

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<MasterCategory>>> GetAllMasterCategories()
        {
            return await _context.MasterCategories
                .Include(mc => mc.McmasterloginNavigation)
                .Include(mc => mc.Mccategory)
                .ToListAsync();
        }

        [HttpPost("create")]
        public async Task<ActionResult<MasterCategory>> CreateMasterCategory(CreateMasterCategoryDto dto)
        {
            var master = await _context.Users
                .FirstOrDefaultAsync(u => u.Userlogin == dto.MasterLogin && u.Userroleid == 2);

            if (master == null)
                return BadRequest("User is not a master or doesn't exist");

            var category = await _context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                return BadRequest("Category doesn't exist");

            var existingLink = await _context.MasterCategories
                .FirstOrDefaultAsync(mc => mc.Mcmasterlogin == dto.MasterLogin && mc.Mccategoryid == dto.CategoryId);

            if (existingLink != null)
                return BadRequest("This master is already linked to this category");

            var masterCategory = new MasterCategory
            {
                Mcmasterlogin = dto.MasterLogin,
                Mccategoryid = dto.CategoryId
            };

            _context.MasterCategories.Add(masterCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMasterCategoryById), new { id = masterCategory.Mcid }, masterCategory);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteMasterCategory(int id)
        {
            var masterCategory = await _context.MasterCategories.FindAsync(id);
            if (masterCategory == null) return NotFound();
            _context.MasterCategories.Remove(masterCategory);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<MasterCategory>> GetMasterCategoryById(int id)
        {
            var masterCategory = await _context.MasterCategories
                .Include(mc => mc.McmasterloginNavigation)
                .Include(mc => mc.Mccategory)
                .FirstOrDefaultAsync(mc => mc.Mcid == id);
            if (masterCategory == null) return NotFound();
            return masterCategory;
        }
    }
    public class CreateMasterCategoryDto
    {
        public string MasterLogin { get; set; } = null!;
        public int CategoryId { get; set; }
    }
}