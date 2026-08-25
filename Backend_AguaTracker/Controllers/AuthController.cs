using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend_AguaTracker.Service.Interfaces;
using Backend_AguaTracker.Domain;
using Microsoft.AspNetCore.Authentication;

namespace Backend_AguaTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User object is null.");
            }

            var result = await _authService.LoginAsync(user.Email, user.Password);
            if (!result.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(new { Token = result.Value });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if(user == null)
            {
                return BadRequest("User object is null.");
            }

            var result = await _authService.RegisterAsync(user);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok(new { Token = result.Value });
        }
    }
}
