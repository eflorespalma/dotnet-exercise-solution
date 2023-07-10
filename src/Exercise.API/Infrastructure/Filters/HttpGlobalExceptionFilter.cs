using Exercise.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Exercise.API.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            this.env = env ?? throw new ArgumentNullException(nameof(env));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(ExerciseBaseException))
            {
                var innerException = context.Exception.InnerException;
                var problemDetails = new ValidationProblemDetails()
                {
                    Title = "Domain Validation",
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "See the errors property for additional details."
                };

                if (innerException == null)
                {
                    problemDetails.Errors.Add("messages", new string[] { context.Exception.Message.ToString() });
                    _logger.LogError("Errors: {ErrorMessage}", context.Exception.Message.ToString());
                }
                else
                {
                    if (innerException is ValidationException)
                    {
                        var errors = ((ValidationException)innerException)?.Errors?.Select(x => x.ErrorMessage)?.ToArray();
                        if (errors != null)
                        {
                            problemDetails.Errors.Add("messages", errors);
                            _logger.LogError("Errors: {ErrorMessage}", errors);
                        }
                    }
                    else
                    {
                        problemDetails.Errors.Add("messages", new string[] { context.Exception.Message.ToString() });
                        _logger.LogError("Errors: {ErrorMessage}", context.Exception.InnerException.Message);
                    }
                }
                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Title = "UnhandleException",
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status500InternalServerError,
                    Detail = "See the errors property for additional details."
                };

                _logger.LogError("Errors: {ErrorMessage}", context.Exception);

                if (env.IsDevelopment())
                    problemDetails.Errors.Add("messages", new string[] { context.Exception.Message.ToString() });
                else
                    problemDetails.Errors.Add("messages", new string[] { "An error has occurred. Please contact the administrator." });

                context.Result = new ObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            context.ExceptionHandled = true;
        }
    }
}
