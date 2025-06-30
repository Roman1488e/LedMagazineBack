using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LedMagazineBack.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class ValidateAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments)
        {
            var argumentType = argument.Value?.GetType();
            if (argumentType == null)
                continue;
            
            if (argumentType.IsPrimitive || argumentType == typeof(string) || argumentType == typeof(Guid))
                continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            var validator = context.HttpContext.RequestServices.GetService(validatorType);

            if (validator != null)
            {
                var validateMethod = validatorType.GetMethod("ValidateAsync", new[] { argumentType, typeof(CancellationToken) });
                var resultTask = (Task)validateMethod.Invoke(validator, new object[] { argument.Value, CancellationToken.None });
                await resultTask.ConfigureAwait(false);

                var resultProperty = resultTask.GetType().GetProperty("Result");
                var validationResult = resultProperty.GetValue(resultTask);

                var isValid = (bool)validationResult?.GetType().GetProperty("IsValid")?.GetValue(validationResult);
                if (isValid) continue;
                var errors = validationResult
                    .GetType()
                    .GetProperty("Errors")
                    ?.GetValue(validationResult) as IEnumerable<FluentValidation.Results.ValidationFailure>;

                var errorDict = errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                context.Result = new BadRequestObjectResult(new
                {
                    Message = "Validation failed",
                    Errors = errorDict
                });
                return;
            }
        }

        await next();
    }
}
