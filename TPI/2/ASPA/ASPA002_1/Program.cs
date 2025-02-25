namespace ASPA002_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseWelcomePage("/aspnetcore");
            app.MapGet("/aspnetcore", () => "��� ��������"); // UseWelcomePage �������� middleware � ������������� ������� �� /aspnetcore ������ MapGet

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Run();
        }
    }
}
