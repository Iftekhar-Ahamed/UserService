using Application.DTOs.APIRequestResponseDTOs;
using Application.DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using UserService.ActionFilters;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public UserController()
        {

        }

        [HttpPost("CreateUser")]
        [ValidateModel]
        public IActionResult CreateNewUser([FromBody]CreateNewUserRequestDto createNewUserRequest)
        {
            ApiResponseDto<string> response = new ApiResponseDto<string>
            {
                Data = null,
                Message = "Successfully Created User",
                Success = true
            };

            return Ok(response);
        }

    }
}