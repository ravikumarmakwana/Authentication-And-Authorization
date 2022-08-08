﻿using Microsoft.AspNetCore.Identity;

namespace JwtAuthenticationAndAuthorization.Entities
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
