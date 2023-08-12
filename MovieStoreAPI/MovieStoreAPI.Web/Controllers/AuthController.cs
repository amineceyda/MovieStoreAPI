using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Business.Models;
using MovieStoreAPI.Business.Services;
using System.Threading.Tasks;

namespace MovieStoreAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var status = await _authService.LoginAsync(request);

            if (status.StatusCode == 1)
            {
                return Ok(new { Message = status.Message });
            }

            return Unauthorized(new { Message = status.Message });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return Ok(new { Message = "Logged out successfully" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            var status = await _authService.RegisterAsync(request);

            if (status.StatusCode == 1)
            {
                return Ok(new { Message = status.Message });
            }

            return BadRequest(new { Message = status.Message });
        }
    }
}
