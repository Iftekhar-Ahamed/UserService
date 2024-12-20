using Application.DTOs.APIRequestResponseDTOs;
using Application.Helpers.BasicDataHelpers;
using FluentValidation;

namespace UserService.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext.Request.ContentType == "application/json")
        {
            var body = await httpContext.Request.ReadFromJsonAsync<object>();
            
            if (body != null)
            {
                var validatorType = typeof(IValidator<>).MakeGenericType(body.GetType());
                var validator = httpContext.RequestServices.GetService(validatorType) as IValidator;

                ErrorResponseDto errorResponse = new ErrorResponseDto
                {
                    Title = "Error with requested parameters",
                    ErrorDetails = new(),
                };
                
                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(body));
                
                    if (!validationResult.IsValid)
                    {
                        errorResponse.ErrorDetails.AddRange(
                            validationResult.Errors
                                .Select(e => new ErrorDescriptionDto
                                {
                                    Key  = e.PropertyName,
                                    Value = e.ErrorMessage
                                }).ToList()
                        );
                    }
                }

                if (errorResponse.ErrorDetails.Any())
                {
                    ApiResponseDto<string> response = new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Validation failed",
                        Error = errorResponse
                    };
                    
                    string content = JsonConvertHelper.ConvertJsonString(errorResponse);
            
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync(content);
                    
                    return;
                }
            }
        }
        
        await next(httpContext);
    }
}