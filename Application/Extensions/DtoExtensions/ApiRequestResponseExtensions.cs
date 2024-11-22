using Application.DTOs.APIRequestResponseDTOs;

namespace Application.Extensions.DtoExtensions;

public static class ApiRequestResponseExtensions
{
    public static void Success<T>( this ApiResponseDto<T> response, string successMessage = "Successful", bool showMessage = false )
    {
        response.Message = successMessage;
        response.ShowMessage = showMessage;
        response.Success = true;
        response.Error = null;
    }
    
    public static void Failed<T>( this ApiResponseDto<T> response, string errorMessage = "Something Bad Happened", bool showMessage = false )
    {
        response.Message = errorMessage;
        response.ShowMessage = showMessage;
        response.Success  = false;
        response.Error = null;
    }
}