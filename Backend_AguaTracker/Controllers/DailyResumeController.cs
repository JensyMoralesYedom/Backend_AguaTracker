using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Service.Interfaces;
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
        public async Task<IActionResult> GetDailyResumesByUserIdAndDate(int userId, DateTime date)
        {
            var result = await _dailyResumeService.GetDailyResumenByUserIdAndDateAsync(userId, date);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDailyResume([FromBody] DailyResume dailyResume)
        {
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
        public async Task<IActionResult> UpdateDailyResume(int id, [FromBody] DailyResume dailyResume)
        {
            if (id != dailyResume.Id)
            {
                return BadRequest("ID mismatch");
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
        public async Task<IActionResult> DeleteDailyResume(int id)
        {
            var existingResumeResult = await _dailyResumeService.GetDailyResumenByIdAsync(id);
            if (!existingResumeResult.IsSuccess)
            {
                return NotFound(existingResumeResult.Error);
            }

            var result = await _dailyResumeService.DeleteDailyResumenAsync(existingResumeResult.Value);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

    }
}
