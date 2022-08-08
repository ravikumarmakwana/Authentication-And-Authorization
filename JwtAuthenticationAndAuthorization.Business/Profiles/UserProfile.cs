using AutoMapper;
using JwtAuthenticationAndAuthorization.Entities;
using JwtAuthenticationAndAuthorization.Models;

namespace JwtAuthenticationAndAuthorization.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationRequest, User>();
            CreateMap<User, UserRegistrationResponse>();

            CreateMap<User, AuthenticationResponse>();
            CreateMap<User, TokenResponse>();
        }
    }
}
