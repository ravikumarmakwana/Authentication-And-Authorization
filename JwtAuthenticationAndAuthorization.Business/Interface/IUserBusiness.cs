using JwtAuthenticationAndAuthorization.Core.Enums;
using JwtAuthenticationAndAuthorization.Models;

namespace JwtAuthenticationAndAuthorization.Business.Interface
{
    public interface IUserBusiness
    {
        Task<UserRegistrationResponse> RegisterAsync(UserRegistrationRequest userRegistrationRequest, Role role);
    }
}
