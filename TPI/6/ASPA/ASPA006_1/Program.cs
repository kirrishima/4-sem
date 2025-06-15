using ASPA006_1;
using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Text.Json;

internal class Program
{

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);
        builder.Services.Configure<CelebritiesConfig>(
            builder.Configuration.GetSection("Celebrities")
        );

        builder.Services.AddScoped<IRepository, Repository>((p) =>
        {
            CelebritiesConfig? config = p.GetService<IOptions<CelebritiesConfig>>()?.Value;
            return new Repository(config?.ConnectionString ?? throw new ArgumentNullException());
        });

        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        app.UseDefaultFiles();

#if RESTRICT_CELEBRITIES
        app.Use(async (context, next) =>
        {
            if (context.Request.Path.StartsWithSegments("/Celebrities"))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }
            await next();
        });
#endif

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                Directory.GetCurrentDirectory(),
                app.Services.GetService<IOptions<CelebritiesConfig>>()?.Value.PhotosFolder ?? throw new ArgumentNullException())
            )
        });

        // ---------------------- ЗНАМЕНИТОСТИ (Celebrities) ----------------------
        var celebrities = app.MapGroup("/api/Celebrities");

        // Все знаменитости
        celebrities.MapGet("/", (IRepository repo) => repo.GetAllCelebrities());

        // Знаменитость по ID
        celebrities.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var result = repo.GetCelebrityById(id);
            if (result is null)
                throw new NotFoundException($"Celebrity with id {id} not found.");
            return Results.Ok(result);
        });

        // События жизни по ID знаменитости
        celebrities.MapGet("/Lifeevents/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var result = repo.GetLifeeventsByCelebrityId(id);
            if (result is null)
                throw new NotFoundException($"Life events for celebrity with id {id} not found.");
            return Results.Ok(result);
        });

        // Удалить знаменитость по ID
        celebrities.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var cel = repo.GetCelebrityById(id);
            if (cel is null || !repo.DelCelebrity(id))
                throw new NotFoundException($"404002:Celebrity Id = {id}.");
            return Results.Ok(cel);
        });

        // Добавить знаменитость
        celebrities.MapPost("/", (IRepository repo, Celebrity celebrity) =>
        {
            if (!repo.AddCelebrity(celebrity))
                throw new BadRequestException("Failed to add celebrity due to invalid data.");
            return Results.Ok(celebrity);
        });

        // Обновить знаменитость по ID
        celebrities.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Celebrity celebrity) =>
        {
            if (!repo.UpdCelebrity(id, celebrity))
                throw new NotFoundException($"Celebrity with id {id} not found for update.");
            return Results.Ok(repo.GetCelebrityById(id));
        });

        // Получить файл по имени
        celebrities.MapGet("/photo/{fname}", (IOptions<CelebritiesConfig> iconfig, HttpContext context, string fname) =>
        {
            var path = Path.Combine("wwwroot", "Celebrities", fname);
            if (!File.Exists(path))
                throw new NotFoundException("File not found.");

            //context.Response.Headers["Content-Disposition"] = $"attachment; filename=\"{fname}\"";
            context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
            context.Response.Headers["Pragma"] = "no-cache";
            context.Response.Headers["Expires"] = "0";

            var contentType = "image/jpeg";
            var fileBytes = File.ReadAllBytes(path);
            return Results.File(fileBytes, contentType);
        });

        // ---------------------- СОБЫТИЯ (Lifeevents) ----------------------
        var lifeevents = app.MapGroup("/api/Lifeevents");

        // Все события
        lifeevents.MapGet("/", (IRepository repo) => repo.GetAllLifeevents());

        // Событие по ID
        lifeevents.MapGet("/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var result = repo.GetLifeeventById(id);
            if (result is null)
                throw new NotFoundException($"Life event with id {id} not found.");
            return Results.Ok(result);
        });

        // События по Celebrity ID
        lifeevents.MapGet("/Celebrities/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var result = repo.GetLifeeventsByCelebrityId(id);
            if (result is null)
                throw new NotFoundException($"Life events for celebrity with id {id} not found.");
            return Results.Ok(result);
        });

        // Удалить событие по ID
        lifeevents.MapDelete("/{id:int:min(1)}", (IRepository repo, int id) =>
        {
            var e = repo.GetLifeeventById(id);
            if (e is null || !repo.DelLifeevent(id))
                throw new NotFoundException($"Life event with id {id} not found for deletion.");
            return Results.Ok(e);
        });

        // Добавить событие
        lifeevents.MapPost("/", (IRepository repo, Lifeevent lifeevent) =>
        {
            if (!repo.AddLifeevent(lifeevent))
                throw new BadRequestException("Failed to add life event");

            return Results.Ok(lifeevent);
        });

        // Обновить событие по ID
        lifeevents.MapPut("/{id:int:min(1)}", (IRepository repo, int id, Lifeevent lifeevent) =>
        {
            if (!repo.UpdLifeevent(id, lifeevent))
                throw new NotFoundException($"Life event with id {id} not found for update.");
            return Results.Ok(repo.GetLifeeventById(id));
        });

        app.Run();
    }
}



public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
