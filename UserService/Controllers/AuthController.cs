using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UserService.ActionFilters;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody]LogInRequestDto logInRequestRequest)
        {
            var res = await authService.LoginAsync(request: logInRequestRequest);

            if (!res.Success)
            {
                return BadRequest(res);
            }
            
            return Ok(res);
        }
    }
}
