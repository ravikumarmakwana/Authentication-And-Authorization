using JwtAuthenticationAndAuthorization.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationAndAuthorization.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<ActionResult> GetAdmin()
        {
            return Ok("Admin");
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("admin-user")]
        public async Task<ActionResult> GetAdminAndUser()
        {
            return Ok("Admin and User");
        }

        [Authorize(Roles = "User")]
        [HttpGet("user")]
        public async Task<ActionResult> GetUser()
        {
            return Ok("User");
        }
    }
}
