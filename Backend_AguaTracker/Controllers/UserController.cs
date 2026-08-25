using Backend_AguaTracker.Domain;
using Backend_AguaTracker.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            
            var userid = GetCurrentUserId();
            if (userid != id)
                return Forbid("You are not authorized to view this user.");

            
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result.Value);
        }


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
           
            var userid = GetCurrentUserId();
            if (userid != id)
                return Forbid("You are not authorized to update this user.");

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
            

            var userid = GetCurrentUserId();
            if (userid != id)
                return Forbid("You are not authorized to delete this user.");


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

        private int GetCurrentUserId()
        {
            // ASP.NET Core ya validó el token, solo extraemos el valor del Claim "UserId"
            var userIdClaim = HttpContext.User.FindFirst("UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

    }
}
