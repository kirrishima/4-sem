using Test_ASPA005_3;
using static System.Net.Mime.MediaTypeNames;

var test = new Test_ASPA005_3.Test();

const string host = "http://localhost:5025";

Console.WriteLine("-- /A --------------------------------------------");
await test.ExecuteGET<int?>($"{host}/A/3", (int? x, int? y, int status) => (x == 3 && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<int?>($"{host}/A/-3", (int? x, int? y, int status) => (x == -3 && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<int?>($"{host}/A/118", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{host}/A/5", (int? x, int? y, int status) => (x == 5 && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{host}/A/-5", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<int?>($"{host}/A/118", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{host}/A/2/3", (int? x, int? y, int status) => (x == 2 && y == 3 && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{host}/A/0/3", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{host}/A/25/-3", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<int?>($"{host}/A/0/-3", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{host}/A/1-99", (int? x, int? y, int status) => (x == 1 && y == 99 && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{host}/A/99-1", (int? x, int? y, int status) => (x == 99 && y == 1 && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{host}/A/1--25", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{host}/A/1--25", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<int?>($"{host}/A/25-101", (int? x, int? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);

Console.WriteLine("-- /B --------------------------------------------");
await test.ExecuteGET<float?>($"{host}/B/2.5", (float? x, float? y, int status) => (x == 2.5 && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<float?>($"{host}/B/2", (float? x, float? y, int status) => (x == 2.0 && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<float?>($"{host}/B/2X", (float? x, float? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePOST<float?>($"{host}/B/2.5/3.2", (float? x, float? y, int status) => (x == 2.5f && y == 3.2f && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<float?>($"{host}/B/2.5-3.2", (float? x, float? y, int status) => (x == 2.5 && y == 3.2 && status == 200) ? Test.OK : Test.NOK);

Console.WriteLine("-- /C --------------------------------------------");
await test.ExecuteGET<bool?>($"{host}/C/2.5", (bool? x, bool? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<bool?>($"{host}/C/true", (bool? x, bool? y, int status) => (x == true && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<bool?>($"{host}/C/true,false", (bool? x, bool? y, int status) => (x == true && y == false && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteDELETE<bool?>($"{host}/C/true,false", (bool? x, bool? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);

Console.WriteLine("-- /D --------------------------------------------");
await test.ExecuteGET<DateTime?>($"{host}/D/2025-02-25", (DateTime? x, DateTime? y, int status) => (x == new DateTime(2025, 02, 25) && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{host}/D/2025-02-29", (DateTime? x, DateTime? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{host}/D/2024-02-29", (DateTime? x, DateTime? y, int status) => (x == new DateTime(2024, 02, 29) && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<DateTime?>($"{host}/D/2025-02-25T19:25", (DateTime? x, DateTime? y, int status) => (x == new DateTime(2025, 02, 25, 19, 25, 0) && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePOST<DateTime?>($"{host}/D/2025-02-25|2025-03-25", (DateTime? x, DateTime? y, int status) => (x == new DateTime(2025, 02, 25) && y == new DateTime(2025, 03, 25) && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<DateTime?>($"{host}/D/2025-02-25T19:25", (DateTime? x, DateTime? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);

Console.WriteLine("\n-- /E -----------------------------------");
await test.ExecuteGET<string?>($"{host}/E/12-bis", (string? x, string? y, int status) => (x == "bis" && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/E/11-bis", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/E/12-777", (string? x, string? y, int status) => (x == "777" && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/E/12-", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{host}/E/abcd", (string? x, string? y, int status) => (x == "abcd" && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{host}/E/abcd123", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{host}/E/a", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{host}/E/123456", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecutePUT<string?>($"{host}/E/aabbccddeeffgghh", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);

Console.WriteLine("\n-- /F -----------------------------------");
await test.ExecuteGET<string?>($"{host}/F/smw@belstu.by", (string? x, string? y, int status) => (x == "smw@belstu.by" && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/F/xxx@yyy.by", (string? x, string? y, int status) => (x == "xxx@yyy.by" && y == null && status == 200) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/F/xxx@yyy.ru", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/F/xxxyyyy.by", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);
await test.ExecuteGET<string?>($"{host}/F/xxx@yyy", (string? x, string? y, int status) => (x == null && y == null && status == 404) ? Test.OK : Test.NOK);

Console.ReadLine();