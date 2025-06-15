using ASPA007_1.Extension;
using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace ASPA007_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureService(builder);

            builder.Services.AddRazorPages(o =>
            {
                o.Conventions.AddPageRoute("/Celebrities", "/");
                o.Conventions.AddPageRoute("/Celebrity", "/Celebrities/{id:int:min(1)}");
                o.Conventions.AddPageRoute("/Celebrity", "/Celebrity/{id:int:min(1)}");
            });

            var app = builder.Build();
            app.UseApiErrorHandling();

            var celebsCfg = app.Services
                .GetRequiredService<IOptions<CelebritiesConfig>>()
                .Value;

            InitDatabase(celebsCfg.ConnectionString);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), celebsCfg.PhotosFolder)
                ),
                RequestPath = celebsCfg.PhotosRequestPath
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.MapCelebrities();
            app.MapLifeevents();

            app.Run();
        }

        private static void InitDatabase(string connectionString)
        {
            var ini = new Init(connectionString);
            Init.Execute();
        }

        private static void ConfigureService(WebApplicationBuilder builder)
        {
            builder.Configuration.AddJsonFile("Celebrities.config.json", optional: false, reloadOnChange: true);
            builder.Services.Configure<CelebritiesConfig>(
                builder.Configuration.GetSection("Celebrities")
            );

            builder.Services.AddScoped<IRepository, Repository>((p) =>
            {
                CelebritiesConfig? config = p.GetService<IOptions<CelebritiesConfig>>()?.Value;
                return new Repository(config?.ConnectionString ?? throw new ArgumentNullException());
            });
        }
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
