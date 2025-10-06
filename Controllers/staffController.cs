using Microsoft.AspNetCore.Mvc;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class staffController : ControllerBase
    {
        spaSalonDbContext context =  new spaSalonDbContext();

        [HttpPost("{staffId}")]
        public IActionResult getStaff(int staffId)
        {
            List<Staff> foundStaff = context.Staff.ToList().Where(u => u.StaffId == staffId).ToList();
            if (foundStaff.Count == 0) return BadRequest(new { message = "Сотрудник с таким ID не найден" });
            
            return Ok(foundStaff.First());
        }
    }
}