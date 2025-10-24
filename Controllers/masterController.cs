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
            Master? foundMaster = context.Masters.FirstOrDefault(m => m.Masterid == masterId);
            if (foundMaster == null) return NotFound(new RequestError { message = "Мастер не найден" });
        
            return Ok(new MasterGetResponse { MasterId = foundMaster.Masterid, MasterUserLogin = foundMaster.Masteruserlogin, MasterSpecialization = foundMaster.Masterspecialization, MasterExperience = foundMaster.Masterexperience,  MasterStatus = foundMaster.Masterstatus });
        }

        [HttpPost("add")]
        [ProducesResponseType<MasterAddResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status400BadRequest)]
        public IActionResult AddMaster([FromBody] MasterAddRequest master)
        {
            Master newMaster = new Master()
            {
                Masteruserlogin = master.MasterUserLogin,
                Masterspecialization = master.MasterSpecialization,
                Masterexperience = master.MasterExperience,
                Masterstatus = master.MasterStatus
            };
            
            context.Masters.Add(newMaster);
            context.SaveChanges();
            
            return Ok(new MasterAddResponse { MasterId = newMaster.Masterid, MasterUserLogin = newMaster.Masteruserlogin, MasterSpecialization = newMaster.Masterspecialization, MasterExperience = newMaster.Masterexperience, MasterStatus = newMaster.Masterstatus });
        }
        
        [HttpPost("{masterId}/rate")]
        [ProducesResponseType<MasterRateResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<RequestError>(StatusCodes.Status404NotFound)]
        public IActionResult RateMaster(int masterId, [FromBody] MasterRateRequest masterRateRequest)
        {
            Rating newRating = new Rating()
            {
                Ratinguserlogin = masterRateRequest.RatingUserLogin,
                Ratingmasterid = masterId,
                Ratingtext = masterRateRequest.RatingText,
                Ratingstars = masterRateRequest.RatingStars
            };
            
            context.Ratings.Add(newRating);
            context.SaveChanges();
            
            return Ok(newRating);
        }
        
        [HttpGet("{masterId}/rating")]
        [ProducesResponseType<List<Rating>>(StatusCodes.Status200OK)]
        public IActionResult RateMaster(int masterId)
        {
            List<Rating> ratings = context.Ratings.Where(r => r.Ratingmasterid == masterId).ToList();
            return Ok(ratings);
        }
    }
}