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

        [HttpGet("{masterId}")]
        [ProducesResponseType<MasterGetResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult GetMaster(int masterId)
        {
            Master? foundMaster = context.Masters.FirstOrDefault(m => m.MasterId == masterId);
            if (foundMaster == null) return NotFound(new RequestError { message = "Мастер не найден" });
        
            return Ok(new MasterGetResponse { MasterId = foundMaster.MasterId, MasterUserLogin = foundMaster.MasterUserLogin, MasterSpecialization = foundMaster.MasterSpecialization, MasterExperience = foundMaster.MasterExperience,  MasterStatus = foundMaster.MasterStatus });
        }

        [HttpPost("add")]
        [ProducesResponseType<MasterAddResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult AddMaster([FromBody] MasterAddRequest master)
        {
            Master newMaster = new Master()
            {
                MasterUserLogin = master.MasterUserLogin,
                MasterSpecialization = master.MasterSpecialization,
                MasterExperience = master.MasterExperience,
                MasterStatus = master.MasterStatus
            };
            
            context.Masters.Add(newMaster);
            context.SaveChanges();
            
            return Ok(new MasterAddResponse { MasterId = newMaster.MasterId, MasterUserLogin = newMaster.MasterUserLogin, MasterSpecialization = newMaster.MasterSpecialization, MasterExperience = newMaster.MasterExperience, MasterStatus = newMaster.MasterStatus });
        }
        
        [HttpPost("{masterId}/rate")]
        [ProducesResponseType<MasterRateResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult RateMaster([FromBody] MasterRateRequest masterRateRequest)
        {
            Rating newRating = new Rating()
            {
                RatingUserLogin = masterRateRequest.RatingUserLogin,
                RatingMasterId = masterRateRequest.RatingMasterId,
                RatingText = masterRateRequest.RatingText,
                RatingStars = masterRateRequest.RatingStars
            };
            
            context.Ratings.Add(newRating);
            context.SaveChanges();
            
            return Ok(newRating);
        }
    }
}