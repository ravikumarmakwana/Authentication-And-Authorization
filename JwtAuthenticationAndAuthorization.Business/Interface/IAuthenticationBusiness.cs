using JwtAuthenticationAndAuthorization.Models;

namespace JwtAuthenticationAndAuthorization.Business.Interface
{
    public interface IAuthenticationBusiness
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest);
        Task<TokenResponse> GetAccessTokenAsync(string refreshToken);
    }
}
