using Microsoft.AspNetCore.HttpLogging;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); // создает билдер веб-приложения

        builder.Services.AddHttpLogging(o => // добавляет сервис http-логирования с параметрами
        {
            o.LoggingFields = HttpLoggingFields.RequestMethod | // логирует метод запроса
                              HttpLoggingFields.RequestPath |    // логирует путь запроса
                              HttpLoggingFields.ResponseStatusCode; // логирует статус код ответа
        });
        builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information); // устанавливает уровень логирования для http-логирования
        var app = builder.Build(); // строит веб-приложение

        app.UseHttpLogging(); // включает http-логирование в конвейере запросов

        app.MapGet("/", () => "Мое первое ASPA"); // маппинг get-запроса на корневой путь

        app.Run(); // запускает приложение
    }
}
