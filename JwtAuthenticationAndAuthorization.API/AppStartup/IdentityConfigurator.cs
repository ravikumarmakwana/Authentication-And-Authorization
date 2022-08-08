using JwtAuthenticationAndAuthorization.Data.Contexts;
using JwtAuthenticationAndAuthorization.Entities;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationAndAuthorization.API.AppStartup
{
    public static class IdentityConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<long>>(option =>
            {
                option.Password.RequireDigit = true;
                option.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
