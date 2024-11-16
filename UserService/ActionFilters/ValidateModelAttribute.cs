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
                Title = "Model Validation Failed",
                Errors = new List<(string Type,string Message)>{ ("Test","Test")},
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
                            errorResponse.Errors.AddRange(
                                validationResult.Errors
                                    .Select(e => (Type: e.PropertyName, Message: e.ErrorMessage))
                                    .ToList()
                            );
                        }
                    }
                }
            }

            if (errorResponse.Errors.Any())
            {
                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}

