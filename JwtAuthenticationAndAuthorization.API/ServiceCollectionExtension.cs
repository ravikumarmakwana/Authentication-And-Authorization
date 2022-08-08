using JwtAuthenticationAndAuthorization.Business.Implementation;
using JwtAuthenticationAndAuthorization.Business.Interface;
using JwtAuthenticationAndAuthorization.Core.Implementation;
using JwtAuthenticationAndAuthorization.Core.Interface;
using JwtAuthenticationAndAuthorization.Data.Implementation;
using JwtAuthenticationAndAuthorization.Data.Interface;

namespace JwtAuthenticationAndAuthorization.API
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IAuthenticationBusiness, AuthenticationBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            return services;
        }
    }
}
