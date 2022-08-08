using JwtAuthenticationAndAuthorization.Business.Interface;
using JwtAuthenticationAndAuthorization.Core.Enums;
using JwtAuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationAndAuthorization.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;

        public UsersController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost("register-admin")]
        public async Task<ActionResult<UserRegistrationResponse>> RegisterAdminAsync(UserRegistrationRequest userRegistrationRequest)
        {
            return Ok(await RegisterAsync(userRegistrationRequest, Role.Admin));
        }

        [HttpPost("register-user")]
        public async Task<ActionResult<UserRegistrationResponse>> RegisterUserAsync(UserRegistrationRequest userRegistrationRequest)
        {
            return Ok(await RegisterAsync(userRegistrationRequest, Role.User));
        }

        private async Task<UserRegistrationResponse> RegisterAsync(UserRegistrationRequest userRegistrationRequest, Role role)
        {
            var userRegistrationResponse = await _userBusiness.RegisterAsync(userRegistrationRequest, role);
            return userRegistrationResponse;
        }
    }
}
