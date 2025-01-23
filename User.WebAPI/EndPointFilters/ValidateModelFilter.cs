using Application.Core.DTOs.APIRequestResponseDTOs;
using FluentValidation;

namespace UserService.EndPointFilters;

public class ValidateModelFilter<T>(IValidator<T> validator) : IEndpointFilter
    where T : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var dto = context.Arguments.FirstOrDefault(arg => arg is T) as T;
        if (dto == null)
        {
            return Results.BadRequest(new { Message = "Invalid request payload." });
        }

        var validationResult = await validator.ValidateAsync(dto);
        if (validationResult.IsValid) return await next(context);
        
        var errorResponse = new ErrorResponseDto
        {
            Title = "Error with requested parameters",
            ErrorDetails = validationResult.Errors
                .Select(e => new ErrorDescriptionDto
                {
                    Key = e.PropertyName,
                    Value = e.ErrorMessage
                }).ToList()
        };

        var response = new ApiResponseDto<string>
        {
            Success = false,
            Message = "Validation failed",
            Error = errorResponse
        };

        return Results.BadRequest(response);

    }
}