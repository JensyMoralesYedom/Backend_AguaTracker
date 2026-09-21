using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace Backend_AguaTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyResumeController : ControllerBase
    {
        private readonly IDailyResumeService _dailyResumeService;

        public DailyResumeController(IDailyResumeService dailyResumeService)
        {
            _dailyResumeService = dailyResumeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllDailyResumes()
        {
            var result = await _dailyResumeService.GetAllDailyResumenAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("date/{date}")]
        [Authorize]
        public async Task<IActionResult> GetDailyResumesByDate(DateTime date)
        {
            var result = await _dailyResumeService.GetDailyResumenByDateAsync(date);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDailyResumeById(int id)
        {
            var result = await _dailyResumeService.GetDailyResumenByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetDailyResumesByUserId(int userId)
        {
            var result = await _dailyResumeService.GetDailyResumenByUserIdAsync(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpGet("user/{userId}/date/{date}")]
        [Authorize]
        public async Task<IActionResult> GetDailyResumesByUserIdAndDate(int userId, DateTime date)
        {
            var result = await _dailyResumeService.GetDailyResumenByUserIdAndDateAsync(userId, date);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Error);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateDailyResume([FromBody] DailyResume dailyResume)
        {
            var userId = GetCurrentUserId();
            dailyResume.UserId = userId;

            var result = await _dailyResumeService.AddDailyResumenAsync
                (
                dailyResume.UserId, 
                dailyResume.Date, 
                dailyResume.TotalIntake, 
                dailyResume.ActivityLevel
                );

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(GetDailyResumeById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateDailyResume(int id, [FromBody] DailyResume dailyResume)
        {
            var userId = GetCurrentUserId();

            if (dailyResume.UserId != userId)
            {
                return Forbid("You are not authorized to update this daily resume.");
            }

            var existingResumeResult = await _dailyResumeService.GetDailyResumenByIdAsync(id);
            if (!existingResumeResult.IsSuccess)
            {
                return NotFound(existingResumeResult.Error);
            }



            var result = await _dailyResumeService.UpdateDailyResumenAsync(dailyResume);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteDailyResume(int id)
        {
            var userId = GetCurrentUserId();
            
            var existingResumeResult = await _dailyResumeService.GetDailyResumenByIdAsync(id);
            var dailyResume = existingResumeResult.Value;
            if (!existingResumeResult.IsSuccess)
            {
                return NotFound(existingResumeResult.Error);
            }

            if (existingResumeResult.Value.UserId != userId)
            {
                return Forbid("You are not authorized to delete this daily resume.");
            }

            var result = await _dailyResumeService.DeleteDailyResumenAsync(existingResumeResult.Value);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            // ASP.NET Core ya validó el token, solo extraemos el valor del Claim "UserId"
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }
    }
}
