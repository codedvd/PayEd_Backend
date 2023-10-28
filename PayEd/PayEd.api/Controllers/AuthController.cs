using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayEd.Core.Services;
using PayEd.Data.Dto;
using PayEd.Data.Dtos;
using PayEd.Infrastructure.Helpers;

namespace PayEd.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _user;
        public AuthController(IUserRepository user)
        {
            _user = user;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto regDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            var response = await _user.CreateUser(regDto);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Inputs");
            }
            var response = await _user.Login(login);
            if (response.Suceeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("Sign-Out")]
        public async Task<IActionResult> SigningOut()
        {
            var response = await _user.SignOut();
            if (response.Suceeded)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
