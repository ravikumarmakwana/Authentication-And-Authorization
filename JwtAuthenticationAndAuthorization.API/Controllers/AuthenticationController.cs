using JwtAuthenticationAndAuthorization.Business.Interface;
using JwtAuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationAndAuthorization.API.Controllers
{
    [ApiController]
    [Route("authenticate")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;

        public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            return Ok(await _authenticationBusiness.AuthenticateAsync(authenticationRequest));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponse>> GenerateAccessTokenAsync(TokenRequest tokenRequest)
        {
            return Ok(await _authenticationBusiness.GetAccessTokenAsync(tokenRequest.RefreshToken));
        }
    }
}
