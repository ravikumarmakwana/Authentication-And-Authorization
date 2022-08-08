using JwtAuthenticationAndAuthorization.Data.Contexts;
using JwtAuthenticationAndAuthorization.Data.Interface;
using JwtAuthenticationAndAuthorization.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthenticationAndAuthorization.Data.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUserByRefreshToken(string refreshToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken && x.RefreshTokenExpiryTime >= DateTime.Now);

            return user;
        }
    }
}
