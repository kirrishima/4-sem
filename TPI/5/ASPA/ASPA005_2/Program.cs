using ASPA005_2;
using DAL004;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

Repository.JSONFileName = "Celebrities.json";

using (IRepository repository = Repository.Create("Celebrities"))
{
    SurnameFilter.Repository = repository;
    PhotoPathFilter.Repository = repository;


    app.UseExceptionHandler("/Celebrities/Error");

    var api = app.MapGroup("/Celebrities");

    api.MapGet("", () => repository.getAllCelebrities());

    api.MapGet("/{id:int}", (int id) =>
    {
        Celebrity? celebrity = repository.getCelebrityById(id);
        if (celebrity == null) throw new FoundByIdException($"Celebrity Id = {id}");
        return celebrity;
    });

    api.MapPost("/", (Celebrity celebrity) =>
    {
        int? id = repository.addCelebrity(celebrity);

        if (id == null)
        {
            throw new AddCelebrityException("/Celebrities error, id == null");
        }

        if (repository.SaveChanges() <= 0)
        {
            throw new SaveException("/Celebrities error, SaveChanges() <= 0");
        }

        return new Celebrity((int)id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
    })
    .AddEndpointFilter<SurnameFilter>()
    .AddEndpointFilter<PhotoPathFilter>();




    api.MapDelete("/{id:int}", (int id) =>
    {
        bool deleted = repository.delCelebrityById(id);
        if (!deleted)
            throw new DeleteByIdException($"Delete by Id:DELETE /Celebrities error, Id = {id}");
        return deleted;
    })
    .AddEndpointFilter<DeleteFilter>();




    api.MapPut("/{id:int}", (int id, Celebrity celebrity) =>
    {
        int? updatedIndex = repository.updCelebrityById(id, celebrity);
        if (!updatedIndex.HasValue)
            throw new UpdateByIdException($"не удалось обновить по id={id}");

        return repository.getCelebrityById(updatedIndex.Value);
    })
    .AddEndpointFilter<PutFilter>();




    app.MapFallback((HttpContext ctx) => Results.NotFound(new { error = $"path {ctx.Request.Path} not supported" }));




    app.Map("/Celebrities/Error", (HttpContext ctx) =>
    {
        Exception? ex = ctx.Features.Get<IExceptionHandlerFeature>()?.Error;

        IResult rc = Results.Problem(detail: ex?.Message, instance: app.Environment.EnvironmentName, title: "ASPA004", statusCode: 500);
        if (ex != null)
        {
            if (ex is CelebrityIsNullException)
                rc = Results.Text($"Value:{ex.Message}", statusCode: 500);

            if (ex is CelebrityIncorrectSurnameException || ex is CelebrityNotFoundException || ex is CelebrityIncorrectFirstnameException)
                rc = Results.Text($"Value:{ex.Message}", statusCode: 409);

            if (ex is FoundByIdException)
                rc = Results.NotFound(ex.Message);

            if (ex is BadHttpRequestException)
                rc = Results.BadRequest(ex.Message);

            if (ex is SaveException)
                rc = Results.Problem(title: "ASPA004/SaveChanges", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);

            if (ex is AddCelebrityException)
                rc = Results.Problem(title: "ASPA004/addCelebrity", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);

            if (ex is DeleteByIdException)
                rc = Results.Problem(title: $"ASPA004/{nameof(Repository.delCelebrityById)}", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);

            if (ex is UpdateByIdException)
                rc = Results.Problem(title: $"ASPA004/{nameof(Repository.updCelebrityById)}", detail: ex.Message, instance: app.Environment.EnvironmentName, statusCode: 500);
        }
        return rc;
    });
    app.Run();
}

public class UpdateByIdException : Exception
{
    public UpdateByIdException(string message) : base($"Update by Id: {message}") { }
}

public class DeleteByIdException : Exception
{
    public DeleteByIdException(string message) : base(message) { }
}

public class FoundByIdException : Exception
{
    public FoundByIdException(string message) : base($"Found by Id: /Celebrities, Celebrity Id = {message}") { }
}

public class SaveException : Exception
{
    public SaveException(string message) : base($"SaveChanges error: {message}") { }
}

public class AddCelebrityException : Exception { public AddCelebrityException(string message) : base($"AddCelebrityException error: {message}") { } };


public class CelebrityIsNullException : Exception
{
    public CelebrityIsNullException() : base("Celebrity cannot be null") { }
    public CelebrityIsNullException(string message) : base(message) { }
    public CelebrityIsNullException(string message, Exception inner) : base(message, inner) { }
}


public class CelebrityIncorrectSurnameException : Exception
{
    public CelebrityIncorrectSurnameException() { }
    public CelebrityIncorrectSurnameException(string message) : base(message) { }
    public CelebrityIncorrectSurnameException(string message, Exception inner) : base(message, inner) { }
}