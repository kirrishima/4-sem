using DAL004;

namespace ASPA005_2
{
    public class SurnameFilter : IEndpointFilter
    {
        public static IRepository? Repository { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var cel = context.GetArgument<Celebrity>(0);

            if (cel is null)
                throw new CelebrityIsNullException();

            if (string.IsNullOrWhiteSpace(cel.Surname) || cel.Surname.Length < 2)
                throw new CelebrityIncorrectSurnameException("POST /Celebrities error, Surname is wrong");

            if (Repository != null && Repository.getCelebritiesBySurname(cel.Surname).Length > 0)
                throw new CelebrityIncorrectSurnameException($"POST /Celebrities error, Surname is doubled");

            return await next(context);
        }
    }


    public class PhotoPathFilter : IEndpointFilter
    {
        public static IRepository? Repository { get; set; }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var cel = context.GetArgument<Celebrity>(0);

            if (cel is null)
                throw new CelebrityIsNullException();

            string fullPath = Path.Combine(DAL004.Repository.JSONFileName, cel.PhotoPath);
            if (!File.Exists(fullPath))
            {
                context.HttpContext.Response.Headers["X-Celebrity"] =
                    $"NotFound = {Path.GetFileName(cel.PhotoPath)}";
            }

            return await next(context);
        }
    }

}

public class PutFilter : IEndpointFilter
{
    public static IRepository? Repository { get; set; }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.GetArgument<int>(0);
        var celebrity = context.GetArgument<Celebrity>(1);

        if (celebrity == null)
        {
            throw new CelebrityIsNullException();
        }

        if (id < 0)
        {
            throw new UpdateByIdException($"PUT /Celebrities error: ID ({id}) is invalid.");
        }

        if (string.IsNullOrWhiteSpace(celebrity.Firstname) || celebrity.Firstname.Length < 2)
        {
            throw new CelebrityIncorrectFirstnameException($"PUT /Celebrities error: Firstname \"{celebrity.Firstname}\" is invalid.");
        }

        if (string.IsNullOrWhiteSpace(celebrity.Surname) || celebrity.Surname.Length < 2)
        {
            throw new CelebrityIncorrectSurnameException($"PUT /Celebrities error: Surname \"{celebrity.Surname}\" is invalid.");
        }

        // Проверяем, существует ли такая запись перед обновлением
        if (Repository?.getCelebrityById(id) == null)
        {
            throw new CelebrityNotFoundException($"PUT /Celebrities error: No celebrity found with ID {id}.");
        }

        return await next(context);
    }

}


public class DeleteFilter : IEndpointFilter
{
    public static IRepository? Repository { get; set; }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var id = context.GetArgument<int>(0);

        if (id < 0)
        {
            throw new DeleteByIdException($"DELETE /Celebrities error, ID is wrong");
        }

        return await next(context);
    }
}


public class CelebrityIncorrectFirstnameException : Exception
{
    public CelebrityIncorrectFirstnameException(string message) : base(message) { }
}


public class CelebrityNotFoundException : Exception
{
    public CelebrityNotFoundException(string message) : base(message) { }
}
