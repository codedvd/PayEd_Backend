using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayEd.Data.Dtos;

namespace PayEd.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto users)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Inputs");
            }
            return Ok(users);
        } 

    }
}
