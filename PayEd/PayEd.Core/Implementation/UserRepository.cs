using PayEd.Data.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PayEd.Core.Services;
using PayEd.Data.AppContext;
using PayEd.Data.Dto;
using PayEd.Data.Dtos;
using PayEd.Data.Enums;
using PayEd.Data.Models;
using PayEd.Infrastructure.Helpers;
using static System.Int32;
using System.Transactions;

namespace PayEd.Core.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, IConfiguration configuration, AppDbContext context, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse> SignOut()
        {
            await _signInManager.SignOutAsync();
            return ApiResponse.Success("", "Sign out successful");
        }


        public async Task<ApiResponse> Login(LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                return ApiResponse.Error("User does not exist");
            }
            var confirm = await _signInManager.CheckPasswordSignInAsync(user, login.Password, lockoutOnFailure: false);

            if (confirm.Succeeded)
            {
                var token = await GenerateTokenAsync(user);
                var response = new Response
                {
                    UserId = user.Id,
                    Fullname = user.Fullname,
                    Token = token,
                };
                return ApiResponse.Success(response, "Login successful");
            }
            else
            {
                return ApiResponse.Error("Invalid password");
            }
        }


        public async Task<ApiResponse> CreateUser(UserRegistrationDto userRequest)
        {
            var user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                return ApiResponse.Error("User already exists");
            }

            user = new User
            {
                User_Id = Guid.NewGuid(),
                User_type = Usertype.Student,
                Fullname = userRequest.FullName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.Phone,
                UserName = userRequest.Email,
                School_name = userRequest.Email,
                Address = userRequest.Email
            };

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var createUserResult = await _userManager.CreateAsync(user, userRequest.Password);

                if (createUserResult.Succeeded)
                {
                    transaction.Complete();
                    return ApiResponse.Success(user, "User added successfully");
                }
                else
                {
                    return ApiResponse.Failed(createUserResult.Errors, "Failed to Register at the moment");
                }
            }
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
