using Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using UserService.ActionFilters;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody]LogInRequestDto logInRequestRequest)
        {
            return BadRequest(logInRequestRequest);
        }
    }
}
