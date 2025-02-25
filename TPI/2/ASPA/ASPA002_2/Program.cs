using Microsoft.Extensions.FileProviders;

namespace ASPA002_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseWelcomePage("/aspnetcore");
            app.MapGet("/aspnetcore", () => "ќно работает"); // UseWelcomePage €вл€етс€ middleware и перехватывает запросы на /aspnetcore раньше MapGet

            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Add("Neumann.html");
            app.UseDefaultFiles(defaultFilesOptions);

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/static",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")) // физическое расположение отличаетс€ от эндпоинта
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/Picture",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Picture"))
            });


            app.Run();
        }
    }
}
