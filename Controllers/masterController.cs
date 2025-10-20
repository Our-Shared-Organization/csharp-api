using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using whatever_api.Model;

namespace whatever_api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class masterController : ControllerBase
    {
        spaSalonDbContext context = new();

        // [HttpGet]
        // [ProducesResponseType<List<Master>>(StatusCodes.Status200OK)]
        // public IActionResult GetAllMasters()
        // {
        //     var masters = context.Masters
        //         .Include(m => m.MasterUser)
        //         .Where(m => m.MasterStatus == true)
        //         .ToList();
        //
        //     return Ok(masters);
        // }

        // [HttpGet("{masterId}")]
        // [ProducesResponseType<Master>(StatusCodes.Status200OK)]
        // [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        // public IActionResult GetMaster(int masterId)
        // {
        //     var master = context.Masters
        //         .Include(m => m.MasterUser)
        //         .FirstOrDefault(m => m.MasterId == masterId);
        //
        //     if (master == null)
        //         return NotFound(new RequestError { message = "Мастер не найден" });
        //
        //     return Ok(master);
        // }

        // [HttpGet("{masterId}/services")]
        // [ProducesResponseType<List<Service>>(StatusCodes.Status200OK)]
        // public IActionResult GetMasterServices(int masterId)
        // {
        //     var services = context.MasterServices
        //         .Where(ms => ms.MsMasterId == masterId)
        //         .Select(ms => ms.MsService)
        //         .Where(s => s.ServiceStatus == true)
        //         .ToList();
        //
        //     return Ok(services);
        // }

        // [HttpPost("add")]
        // [ProducesResponseType<Master>(StatusCodes.Status200OK)]
        // [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        // public IActionResult AddMaster([FromBody] MasterAddRequest request)
        // {
        //     // Проверяем, существует ли пользователь
        //     var user = context.Users.FirstOrDefault(u => u.UserLogin == request.MasterUserLogin);
        //     if (user == null)
        //         return BadRequest(new RequestError { message = "Пользователь не найден" });
        //
        //     // Проверяем, не является ли уже пользователь мастером
        //     var existingMaster = context.Masters.FirstOrDefault(m => m.MasterUserLogin == request.MasterUserLogin);
        //     if (existingMaster != null)
        //         return BadRequest(new RequestError { message = "Данный пользователь уже является мастером" });
        //
        //     var newMaster = new Master()
        //     {
        //         MasterUserLogin = request.MasterUserLogin,
        //         MasterSpecialization = request.MasterSpecialization,
        //         MasterExperience = request.MasterExperience,
        //         MasterRating = 0.00m,
        //         MasterStatus = true
        //     };
        //
        //     context.Masters.Add(newMaster);
        //     context.SaveChanges();
        //
        //     return Ok(newMaster);
        // }
        //
        // [HttpPost("{masterId}/addService")]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        // public IActionResult AddServiceToMaster(int masterId, int serviceId)
        // {
        //     var master = context.Masters.FirstOrDefault(m => m.MasterId == masterId);
        //     if (master == null)
        //         return BadRequest(new RequestError { message = "Мастер не найден" });
        //
        //     var service = context.Services.FirstOrDefault(s => s.ServiceId == serviceId);
        //     if (service == null)
        //         return BadRequest(new RequestError { message = "Услуга не найдена" });
        //
        //     // Проверяем, не добавлена ли уже услуга
        //     var existingRelation = context.MasterServices
        //         .FirstOrDefault(ms => ms.MsMasterId == masterId && ms.MsServiceId == serviceId);
        //
        //     if (existingRelation != null)
        //         return BadRequest(new RequestError { message = "Данная услуга уже добавлена мастеру" });
        //
        //     var masterService = new MasterService()
        //     {
        //         MsMasterId = masterId,
        //         MsServiceId = serviceId
        //     };
        //
        //     context.MasterServices.Add(masterService);
        //     context.SaveChanges();
        //
        //     return Ok();
        // }
        //
        // [HttpPatch("{masterId}/edit")]
        // public IActionResult EditMaster(int masterId, string specialization, int experience)
        // {
        //     var master = context.Masters.FirstOrDefault(m => m.MasterId == masterId);
        //     if (master == null) return NotFound();
        //
        //     master.MasterSpecialization = specialization;
        //     master.MasterExperience = experience;
        //
        //     context.SaveChanges();
        //     return Ok();
        // }
        //
        // [HttpPatch("{masterId}/rating")]
        // public IActionResult UpdateMasterRating(int masterId, decimal rating)
        // {
        //     var master = context.Masters.FirstOrDefault(m => m.MasterId == masterId);
        //     if (master == null) return NotFound();
        //
        //     master.MasterRating = rating;
        //
        //     context.SaveChanges();
        //     return Ok();
        // }
        //
        // [HttpGet("top")]
        // [ProducesResponseType<List<Master>>(StatusCodes.Status200OK)]
        // public IActionResult GetTopMasters(int count = 5)
        // {
        //     var topMasters = context.Masters
        //         .Include(m => m.MasterUser)
        //         .Where(m => m.MasterStatus == true)
        //         .OrderByDescending(m => m.MasterRating)
        //         .Take(count)
        //         .ToList();
        //
        //     return Ok(topMasters);
        // }
    }
}