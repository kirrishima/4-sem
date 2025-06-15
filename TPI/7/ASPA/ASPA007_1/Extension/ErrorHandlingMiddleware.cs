namespace ASPA007_1.Extension
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
                IResult result = ex switch
                {
                    NotFoundException _ => Results.Problem(
                                                title: "Not Found",
                                                detail: ex.Message,
                                                instance: _env.EnvironmentName,
                                                statusCode: 404),
                    BadRequestException _ => Results.Problem(
                                                title: "Bad Request",
                                                detail: ex.Message,
                                                instance: _env.EnvironmentName,
                                                statusCode: 400),
                    _ => Results.Problem(
                                                title: "Internal Server Error",
                                                detail: ex.Message,
                                                instance: _env.EnvironmentName,
                                                statusCode: 500),
                };

                context.Response.ContentType = "application/problem+json";
                await result.ExecuteAsync(context);
            }
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiErrorHandling(this IApplicationBuilder app)
        {
            return app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api"),
                builder => builder.UseMiddleware<ErrorHandlingMiddleware>()
            );
        }
    }
}
