using System.Net;
using System.Text.Json;
using FluentValidation;
using TestLetshare.Application.Common.Models;

namespace TestLetshare.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An exception occurred.");

                var response = context.Response;
                response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                response.StatusCode = statusCode;

                var errorMessage = ex is ValidationException validationEx
                    ? string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage))
                    : ex.Message;

                var apiResponse = ApiResponse<string>.FailureResponse(errorMessage);

                var json = JsonSerializer.Serialize(apiResponse);

                await response.WriteAsync(json);
            }
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
