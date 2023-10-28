using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PayEd.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Int32;

namespace PayEd.Infrastructure.Helpers
{
    public class GenerateJwtToken
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public GenerateJwtToken(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrWhiteSpace(user.Fullname))
                authClaims.Add(new Claim(ClaimTypes.GivenName, user.Fullname));

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                authClaims.Add(new Claim(ClaimTypes.MobilePhone, user.PhoneNumber));

            // Get the roles of the logged-in user
            var roles = await _userManager.GetRolesAsync(user);

            // Add the user's roles as a comma-separated string claim
            authClaims.Add(new Claim("roles", string.Join(",", roles)));

            var signingKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty));
            TryParse(_configuration["JwtSettings:TokenValidityInMinutes"], out var tokenValidityInMinutes);

            // Specify JWTSecurityToken Parameters
            var token = new JwtSecurityToken
            (
                audience: _configuration["JwtSettings:Audience"],
                issuer: _configuration["JwtSettings:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
