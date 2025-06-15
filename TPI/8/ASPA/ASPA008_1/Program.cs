using DAL_Celebrity_MSSQL;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using ASPA008_1.Extension;
using System.Reflection.Metadata;


namespace ASPA008_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureService(builder);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseApiErrorHandling();

            var celebsCfg = app.Services
                .GetRequiredService<IOptions<CelebritiesConfig>>()
                .Value;

            InitDatabase(celebsCfg.ConnectionString);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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

            app.MapCelebrities();
            app.MapLifeevents();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Celebrities}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "celebrity",
                pattern: "/{id:int:min(1)}",
                defaults: new { Controller = "Celebrities", Action = "Human" });
            app.MapControllerRoute(
                name: "celebrity",
                pattern: "/0",
                defaults: new { Controller = "Celebrities", Action = "NewHumanForm" });

            app.Run();
        }

        public static void InitDatabase(string connectionString)
        {
            var ini = new Init(connectionString);
            Init.Execute();
        }

        public static void ConfigureService(WebApplicationBuilder builder)
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

            builder.Services.AddSingleton<CelebrityTitles>();
            builder.Services.AddSingleton<CountryCodes>((p) => new CountryCodes(p.GetRequiredService<IOptions<CelebritiesConfig>>().Value.ISO3166alpha2Path));
        }
    }

    public class CelebritiesConfig
    {
        public string PhotosRequestPath { get; set; } = string.Empty;
        public string PhotosFolder { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string ISO3166alpha2Path { get; set; } = string.Empty;
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }

}
