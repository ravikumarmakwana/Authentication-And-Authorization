using AutoMapper;
using JwtAuthenticationAndAuthorization.Business.Interface;
using JwtAuthenticationAndAuthorization.Core.Enums;
using JwtAuthenticationAndAuthorization.Entities;
using JwtAuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationAndAuthorization.Business.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserBusiness(
            UserManager<User> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserRegistrationResponse> RegisterAsync(UserRegistrationRequest userRegistrationRequest, Role role)
        {
            var userExists = await _userManager.FindByNameAsync(userRegistrationRequest.UserName);
            if (userExists != null)
            {
                throw new InvalidOperationException("User already exists");
            }

            var user = _mapper.Map<User>(userRegistrationRequest);
            var result = await _userManager.CreateAsync(user, userRegistrationRequest.Password);
            await _userManager.AddToRoleAsync(user, role.ToString());

            var response = _mapper.Map<UserRegistrationResponse>(user);
            response.Message =
                result.Succeeded ? "Registration Successful" : "Registration Failed";

            return response;
        }
    }
}
