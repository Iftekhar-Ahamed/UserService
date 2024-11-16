using Application.DTOs.APIRequestResponseDTOs;
using Application.Helpers.BasicDataHelpers;

namespace UserService.Middlewares;

public  class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            ApiResponseDto<string> errorResponse = new ApiResponseDto<string>
            {
                Data = null,
                Message = "Something went wrong",
                ShowMessage = false,
                Error = new ErrorResponseDto
                {
                    Title = "Something went wrong",
                    ErrorDetails = 
                    [
                        new ErrorDescriptionDto
                        {
                            Key = "System.Exception",
                            Value = e.Message
                        }
                    ]
                }
            };
            
            string content = JsonConvertHelper.ConvertJsonString(errorResponse);
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = 500;
            await httpContext.Response.WriteAsync(content);
        }
    }
}