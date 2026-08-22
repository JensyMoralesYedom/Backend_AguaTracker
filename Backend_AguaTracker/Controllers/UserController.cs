using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_AguaTracker.Service.Interfaces;
using Backend_AguaTracker.Domain;

namespace Backend_AguaTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("User object is null.");

            var result = await _userService.AddUserAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
                return BadRequest("User object is null.");

            if (id != user.Id)
                return BadRequest("User ID mismatch between route and body.");

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (!existingUser.IsSuccess)
                return NotFound("User not found.");

            var result = await _userService.UpdateUserAsync(user);
            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUser = await _userService.GetUserByIdAsync(id);
            if (!existingUser.IsSuccess)
                return NotFound("User not found.");
            
            var result = await _userService.DeleteUserAsync(existingUser.Value);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            
            return NoContent();
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmailAsync(email);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }

        

    }
}
