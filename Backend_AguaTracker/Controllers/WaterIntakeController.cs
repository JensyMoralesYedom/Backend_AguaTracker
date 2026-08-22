using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Service.Interfaces;

namespace Backend_AguaTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterIntakeController : ControllerBase
    {
        private readonly IWaterIntakeService _waterIntakeService;

        public WaterIntakeController(IWaterIntakeService waterIntakeService)
        {
            _waterIntakeService = waterIntakeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _waterIntakeService.GetWaterIntakeByIdAsync(id);
            return Ok(result.Value);
        }

        [HttpGet("user/{userid}")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            var result = await _waterIntakeService.GetWaterIntakesByUserIdAsync(userid);
            return Ok(result);
        }

        [HttpPost("user/{userid}/date/{date}")]
        public async Task<IActionResult> CreateWaterIntake([FromBody] WaterIntake waterIntake, int userid, DateTime date)
        {
            if (waterIntake == null)
                return BadRequest("Water intake object is null.");
            var result = await _waterIntakeService.
                                    AddWaterIntakeAsync(userid, (int)waterIntake.Amount, date);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWaterIntake(int id)
        {
            var result = await _waterIntakeService.DeleteWaterIntakeAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWaterIntake(int id, [FromBody] WaterIntake waterIntake)
        {
            if(waterIntake.Id != id)
            {
                return BadRequest("Water intake ID mismatch.");
            }

            var existingWaterIntake = await _waterIntakeService.GetWaterIntakeByIdAsync(id);
            
            if (!existingWaterIntake.IsSuccess)
                return NotFound("Water intake not found.");


            if (waterIntake == null)
                return BadRequest("Water intake object is null.");

            var result = await _waterIntakeService.UpdateWaterIntakeAsync(waterIntake);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return NoContent();
        }
    }
}
