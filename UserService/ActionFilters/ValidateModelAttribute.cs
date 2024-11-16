using Application.DTOs.APIRequestResponseDTOs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserService.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ErrorResponseDto errorResponse = new ErrorResponseDto
            {
                Title = "Error with requested parameters",
                ErrorDetails = new(),
            };
            
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument != null)
                {
                    var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                    var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                    if (validator != null)
                    {
                        var validationResult = validator.Validate(new ValidationContext<object>(argument));
                        
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
                
                context.Result = new BadRequestObjectResult(response);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}

