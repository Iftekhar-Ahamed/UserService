using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.DTOs.APIRequestResponseDTOs;
using Application.Extensions.DtoExtensions;
using Application.Helpers.EncryptionDecryptionHelper;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using User.Core.DTOs.UserDTOs;
using User.Core.Interfaces;

namespace Infrastructure.Services;

public class AuthService(IUserInfoRepository userInfoRepository, IAppConfigService appConfigService) : IAuthService
{
    public async Task<ApiResponseDto<LogInResponseDto>> LoginAsync(LogInRequestDto request)
    {
        var apiResponse = new ApiResponseDto<LogInResponseDto>();
        
        TblUserInformation? userInfo = await userInfoRepository.GetUserByEmailAsync(email: request.UserName);

        if (userInfo == null)
        {
            apiResponse.Message = "Username is invalid";
            apiResponse.Success = false;
        }
        else
        {
            if (string.IsNullOrEmpty(userInfo.Password))
            {
                apiResponse.Failed("Please set your password before trying to login",true);
                return apiResponse;
            }
            
            if (!OneWayEncryptionHelper.IsValidPassword(request.Password, userInfo.Password))
            {
                apiResponse.Message = "Wrong Password";
                apiResponse.Success = false;
            }
            else
            {
                apiResponse.Data = new LogInResponseDto
                {
                    AccessToken = await CreateAccessToken(userId:userInfo.UserId.ToString(),userRoles: new()),
                    RefreshToken = await CreateRefreshToken(userId:userInfo.UserId.ToString(),userRoles: new()),
                };
                apiResponse.Success("Welcome User.Infrastructure");
            }
        }
        
        return apiResponse;
    }

    public async Task<string> CreateAccessToken(string userId, List<string> userRoles)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( await appConfigService.GetJwtSecretKeyAsync()));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userId),
            new (ClaimTypes.Role, string.Join(",", userRoles))
        };
        
        var expirationTime = DateTime.UtcNow.AddHours(1);

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            expires: expirationTime,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public async Task<string> CreateRefreshToken(string userId, List<string> userRoles)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(await appConfigService.GetJwtSecretKeyAsync()));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userId),
            new (ClaimTypes.Role, string.Join(",", userRoles))
        };
        
        var expirationTime = DateTime.UtcNow.AddDays(1);

        var jwtToken = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: expirationTime,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}