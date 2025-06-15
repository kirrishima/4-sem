using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.Options;

namespace ASPA007_1.Extension
{
    public static class WebApplicationCelebritiesExtension
    {
        public static void MapCelebrities(this WebApplication application)
        {
            // ---------------------- ЗНАМЕНИТОСТИ (Celebrities) ----------------------
            var celebrities = application.MapGroup("/api/Celebrities");

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
                var path = Path.Combine(iconfig.Value.PhotosFolder, fname);
                if (!File.Exists(path))
                    return Results.NotFound($"File {fname} not found.");

                context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
                context.Response.Headers["Pragma"] = "no-cache";
                context.Response.Headers["Expires"] = "0";

                var contentType = "image/jpeg";
                var fileBytes = File.ReadAllBytes(path);
                return Results.File(fileBytes, contentType);
            });
        }
    }
}
