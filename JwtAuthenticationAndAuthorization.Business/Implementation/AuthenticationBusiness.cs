using AutoMapper;
using JwtAuthenticationAndAuthorization.Business.Interface;
using JwtAuthenticationAndAuthorization.Core.Helper;
using JwtAuthenticationAndAuthorization.Core.Interface;
using JwtAuthenticationAndAuthorization.Data.Interface;
using JwtAuthenticationAndAuthorization.Entities;
using JwtAuthenticationAndAuthorization.Models;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationAndAuthorization.Business.Implementation
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly UserManager<User> _userManager;
        //private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly IApplicationConfiguration _applicationConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationBusiness(
            UserManager<User> userManager,
            IApplicationConfiguration applicationConfiguration,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _applicationConfiguration = applicationConfiguration;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest authenticationRequest)
        {
            var user = await _userManager.FindByNameAsync(authenticationRequest.UserName);
            await ValidateUserCredentials(authenticationRequest, user);

            var accessToken = await GenerateAccessTokenAsync(user);
            
            var authenticationResponse = _mapper.Map<AuthenticationResponse>(user);
            authenticationResponse.AccessToken = accessToken;

            return authenticationResponse;
        }

        public async Task<TokenResponse> GetAccessTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByUserByRefreshToken(refreshToken);
            if (user == null)
            {
                throw new InvalidOperationException($"Refresh Token is Expired.");
            }

            var accessToken = await GenerateAccessTokenAsync(user);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        private async Task ValidateUserCredentials(AuthenticationRequest authenticationRequest, User user)
        {
            if (user == null)
            {
                throw new InvalidOperationException($"User not exists for given UserName: {authenticationRequest.UserName}");
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, authenticationRequest.Password);
            if (!isPasswordCorrect)
            {
                throw new InvalidOperationException("Invalid Password");
            }
        }

        private async Task<string> GenerateAccessTokenAsync(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = TokenUtility.GetUserClaims(user, userRoles);
            var accessToken = TokenUtility.GenerateAccessToken(claims, _applicationConfiguration);
            await SaveRefreshTokenAsync(user);

            return accessToken;
        }

        private async Task SaveRefreshTokenAsync(User user)
        {
            user.RefreshToken = TokenUtility.GenerateRefreshToken(user);
            user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(_applicationConfiguration.RefreshTokenValidityInMinutes);
            await _userManager.UpdateAsync(user);
        }
    }
}
