using DAL_Celebrity_MSSQL;

internal class Program
{
    private static void Main(string[] args)
    {
        string CS = @"Server=localhost;Database=ASPA06;Trusted_Connection=True;TrustServerCertificate=True;";

        Init init = new Init(CS);
        Init.Execute(delete: true, create: true);

        Func<Celebrity, string> printC = (c) => $"{c.Id}, {c.FullName}, Nationality = {c.Nationality}, ReqPhotoPath = {c.ReqPhotoPath}";
        Func<Lifeevent, string> printL = (l) => $"{l.Id}, CelebrityId = {l.CelebrityId}, Date = {l.Date}, Description = {l.Description}, ReqPhotoPath = {l.ReqPhotoPath}";
        Func<string, string> puri = s => $"images\\{s}";

        using (IRepository repo = Repository.Create(CS))
        {
            // GetAllCelebrities
            {
                Console.WriteLine("------- GetAllCelebrities() -------");
                repo.GetAllCelebrities().ForEach(celebrity => Console.WriteLine(printC(celebrity)));
            }

            // GetAllLifeevents
            {
                Console.WriteLine("------- GetAllLifeevents() -------");
                repo.GetAllLifeevents().ForEach(life => Console.WriteLine(printL(life)));
            }

            // AddCelebrity - Einstein
            {
                Console.WriteLine("------- AddCelebrity() -------");
                Celebrity c = new Celebrity { FullName = "Albert Einstein", Nationality = "DE", ReqPhotoPath = puri("Einstein.jpg") };
                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity: {printC(c)}");
                else Console.WriteLine($"ERROR: AddCelebrity: {printC(c)}");
            }

            // AddCelebrity - Huntington
            {
                Console.WriteLine("------- AddCelebrity() -------");
                Celebrity c = new Celebrity { FullName = "Samuel Huntington", Nationality = "US", ReqPhotoPath = puri("Huntington.jpg") };
                if (repo.AddCelebrity(c)) Console.WriteLine($"OK: AddCelebrity: {printC(c)}");
                else Console.WriteLine($"ERROR: AddCelebrity: {printC(c)}");
            }

            // DelCelebrity
            {
                Console.WriteLine("------- DelCelebrity() -------");
                int id = repo.GetCelebrityIdByName("Einstein");
                if (id > 0)
                {
                    if (repo.DelCelebrity(id)) Console.WriteLine($"OK: DelCelebrity by Id = {id}");
                    else Console.WriteLine($"ERROR: DelCelebrity Id = {id}");
                }
                else Console.WriteLine("ERROR: GetCelebrityIdByName returned 0");
            }

            // UpdCelebrity
            {
                Console.WriteLine("------- UpdCelebrity() -------");
                int id = repo.GetCelebrityIdByName("Huntington");
                if (id > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c != null)
                    {
                        c.Nationality = "RU";
                        if (repo.UpdCelebrity(id, c)) Console.WriteLine($"OK: UpdCelebrity: {printC(c)}");
                        else Console.WriteLine($"ERROR: UpdCelebrity: {printC(c)}");
                    }
                    else Console.WriteLine($"ERROR: GetCelebrityById returned null for id = {id}");
                }
                else Console.WriteLine("ERROR: GetCelebrityIdByName returned 0");
            }

            // AddLifeevent
            {
                Console.WriteLine("------- AddLifeevent() -------");
                int id = repo.GetCelebrityIdByName("Huntington");
                if (id > 0)
                {
                    Lifeevent l1 = new Lifeevent
                    {
                        CelebrityId = id,
                        Date = new DateTime(1927, 4, 18),
                        Description = "Дата рождения",
                        ReqPhotoPath = puri("Huntington.jpg")
                    };

                    Lifeevent l3 = new Lifeevent
                    {
                        CelebrityId = id,
                        Date = new DateTime(1927, 4, 18),
                        Description = "Дата рождения",
                        ReqPhotoPath = puri("Huntington.jpg")
                    };

                    Lifeevent l2 = new Lifeevent
                    {
                        CelebrityId = id,
                        Date = new DateTime(2008, 12, 24),
                        Description = "Дата рождения",
                        ReqPhotoPath = puri("Huntington.jpg")
                    };

                    if (repo.AddLifeevent(l1)) Console.WriteLine($"OK: AddLifeevent: {printL(l1)}");
                    else Console.WriteLine($"ERROR: AddLifeevent: {printL(l1)}");

                    if (repo.AddLifeevent(l3)) Console.WriteLine($"OK: AddLifeevent: {printL(l3)}");
                    else Console.WriteLine($"ERROR: AddLifeevent: {printL(l3)}");

                    if (repo.AddLifeevent(l2)) Console.WriteLine($"OK: AddLifeevent: {printL(l2)}");
                    else Console.WriteLine($"ERROR: AddLifeevent: {printL(l2)}");
                }
                else Console.WriteLine("ERROR: GetCelebrityIdByName returned 0");
            }

            // DelLifeevent
            {
                Console.WriteLine("------- DelLifeevent() -------");
                int id = 22;
                if (repo.DelLifeevent(id)) Console.WriteLine($"OK: DelLifeevent: {id}");
                else Console.WriteLine($"ERROR: DelLifeevent: Id = {id}");
            }

            // UpdLifeevent
            {
                Console.WriteLine("------- UpdLifeevent() -------");
                int id = 23;
                Lifeevent? l1 = repo.GetLifeeventById(id);
                if (l1 != null)
                {
                    l1.Description = "Дата смерти";
                    if (repo.UpdLifeevent(id, l1)) Console.WriteLine($"OK: UpdLifeevent {id}, {printL(l1)}");
                    else Console.WriteLine($"ERROR: UpdLifeevent {id}, {printL(l1)}");
                }
                else Console.WriteLine($"ERROR: GetLifeeventById returned null for id = {id}");
            }

            // GetLifeeventsByCelebrityId
            {
                Console.WriteLine("------- GetLifeeventsByCelebrityId() -------");
                int id = repo.GetCelebrityIdByName("Huntington");
                if (id > 0)
                {
                    Celebrity? c = repo.GetCelebrityById(id);
                    if (c != null)
                    {
                        repo.GetLifeeventsByCelebrityId(c.Id).ForEach(l =>
                            Console.WriteLine($"OK: GetLifeeventsByCelebrityId, CelebrityId = {id}, {printL(l)}"));
                    }
                    else Console.WriteLine($"ERROR: GetCelebrityById returned null for id = {id}");
                }
                else Console.WriteLine("ERROR: GetCelebrityIdByName returned 0");
            }

            // GetCelebrityByLifeeventId
            {
                Console.WriteLine("------- GetCelebrityByLifeeventId() -------");
                int id = 23;
                Celebrity? cc = repo.GetCelebrityByLifeeventId(id);
                if (cc != null) Console.WriteLine($"OK: GetCelebrityByLifeeventId: {printC(cc)}");
                else Console.WriteLine($"ERROR: GetCelebrityByLifeeventId, id = {id}");
            }
        }

        Console.WriteLine("---------------");
        Console.ReadKey();
    }
}
