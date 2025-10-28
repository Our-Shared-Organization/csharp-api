using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        public spaSalonDbContext _context = new();

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAllServices()
        {
            return await _context.Services
                .Include(s => s.Servicecategory)
                .ToListAsync();
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<Service>> GetServiceById(int id)
        {
            var service = await _context.Services
                .Include(s => s.Servicecategory)
                .FirstOrDefaultAsync(s => s.Serviceid == id);
            if (service == null) return NotFound();
            return service;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Service>> CreateService(CreateServiceDto dto)
        {
            var category = await _context.Categories.FindAsync(dto.ServiceCategoryId);
            if (category == null)
                return BadRequest("Category doesn't exist");

            if (dto.ServicePrice < 0)
                return BadRequest("Price cannot be negative");

            if (dto.ServiceDuration <= 0)
                return BadRequest("Duration must be positive");

            var service = new Service
            {
                Servicename = dto.ServiceName,
                Servicedescription = dto.ServiceDescription,
                Serviceduration = dto.ServiceDuration,
                Serviceprice = dto.ServicePrice,
                Servicecategoryid = dto.ServiceCategoryId,
            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetServiceById), new { id = service.Serviceid }, service);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateService(int id, UpdateServiceDto dto)
        {
            var existingService = await _context.Services.FindAsync(id);
            if (existingService == null) return NotFound();

            var category = await _context.Categories.FindAsync(dto.ServiceCategoryId);
            if (category == null)
                return BadRequest("Category doesn't exist");

            if (dto.ServicePrice < 0)
                return BadRequest("Price cannot be negative");

            if (dto.ServiceDuration <= 0)
                return BadRequest("Duration must be positive");

            existingService.Servicename = dto.ServiceName;
            existingService.Servicedescription = dto.ServiceDescription;
            existingService.Serviceduration = dto.ServiceDuration;
            existingService.Serviceprice = dto.ServicePrice;
            existingService.Servicecategoryid = dto.ServiceCategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public class UpdateServiceDto
    {
        public string ServiceName { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public int ServiceDuration { get; set; }
        public decimal ServicePrice { get; set; }
        public int ServiceCategoryId { get; set; }
    }
    public class CreateServiceDto
    {
        public string ServiceName { get; set; } = null!;
        public string? ServiceDescription { get; set; }
        public int ServiceDuration { get; set; }
        public decimal ServicePrice { get; set; }
        public int ServiceCategoryId { get; set; }
    }
}