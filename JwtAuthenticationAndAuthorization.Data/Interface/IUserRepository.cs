using JwtAuthenticationAndAuthorization.Entities;

namespace JwtAuthenticationAndAuthorization.Data.Interface
{
    public interface IUserRepository
    {
        Task<User> GetByUserByRefreshToken(string refreshToken);
    }
}
