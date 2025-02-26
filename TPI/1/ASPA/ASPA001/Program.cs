using Microsoft.AspNetCore.HttpLogging;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args); // ������� ������ ���-����������

        builder.Services.AddHttpLogging(o => // ��������� ������ http-����������� � �����������
        {
            o.LoggingFields = HttpLoggingFields.RequestMethod | // �������� ����� �������
                              HttpLoggingFields.RequestPath |    // �������� ���� �������
                              HttpLoggingFields.ResponseStatusCode; // �������� ������ ��� ������
        });
        builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information); // ������������� ������� ����������� ��� http-�����������
        var app = builder.Build(); // ������ ���-����������

        app.UseHttpLogging(); // �������� http-����������� � ��������� ��������

        app.MapGet("/", () => "��� ������ ASPA"); // ������� get-������� �� �������� ����

        app.Run(); // ��������� ����������
    }
}
