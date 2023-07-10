using Exercise.Domain.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exercise.API.Infrastructure.Filters
{
    public class FluentValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var failures = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Where(error => error != null)
                        .ToList();

                if (failures.Any())
                {
                    var validationFailures = failures.Select(x => new ValidationFailure() { ErrorMessage = x.ErrorMessage }).ToList();

                    string errors = string.Join(",", failures.Select(x => x.ErrorMessage).ToArray());
                    throw new ExerciseBaseException($"{errors}", new ValidationException("Exception for FluentValidation", validationFailures));
                }
            }
            await next();
        }
    }
}
