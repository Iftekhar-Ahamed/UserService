using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserService.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
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
                            context.Result = new BadRequestObjectResult(validationResult.Errors.Select(e => new
                            {
                                Property = e.PropertyName,
                                Error = e.ErrorMessage
                            }));
                            return;
                        }
                    }
                }
            }
            base.OnActionExecuting(context);
        }
    }
}

