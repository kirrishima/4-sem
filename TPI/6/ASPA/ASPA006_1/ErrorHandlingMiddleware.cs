using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASPA006_1
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                IResult result;

                if (ex is NotFoundException)
                {
                    result = Results.Problem(
                        title: "Not Found",
                        detail: ex.Message,
                        instance: _env.EnvironmentName,
                        statusCode: 404);
                }
                else if (ex is BadRequestException)
                {
                    result = Results.Problem(
                        title: "Bad Request",
                        detail: ex.Message,
                        instance: _env.EnvironmentName,
                        statusCode: 400);
                }
                else
                {
                    result = Results.Problem(
                        title: "Internal Server Error",
                        detail: ex.Message,
                        instance: _env.EnvironmentName,
                        statusCode: 500);
                }

                context.Response.ContentType = "application/problem+json";
                await result.ExecuteAsync(context);
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
