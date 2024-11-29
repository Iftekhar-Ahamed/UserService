using Application.DTOs.UserDTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using UserService.ActionFilters;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserInfoService userInfoService) : ControllerBase
    {
        [HttpPost("CreateUser")]
        [ValidateModel]
        public async Task<IActionResult> CreateNewUser([FromBody]CreateNewUserRequestDto createNewUserRequest)
        {
            var response = await userInfoService.CreateNewUserAsync(userInfo: createNewUserRequest);
            
            if (response.Success)
            {
                return Ok(response);
            }
            
            return BadRequest(response);
        }
        
        [HttpPost("UpdateUser")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUser([FromBody]CreateNewUserRequestDto createNewUserRequest)
        {
            var response = await userInfoService.CreateNewUserAsync(userInfo: createNewUserRequest);
            
            if (response.Success)
            {
                return Ok(response);
            }
            
            return BadRequest(response);
        }


    }
}