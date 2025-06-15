using DAL_Celebrity_MSSQL;

namespace ASPA008_1.Extension
{
    public static class WebApplicationLifeeventsExtension
    {
        public static void MapLifeevents(this WebApplication application)
        {
            // ---------------------- СОБЫТИЯ (Lifeevents) ----------------------
            var lifeevents = application.MapGroup("/api/Lifeevents");

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
        }
    }
}
