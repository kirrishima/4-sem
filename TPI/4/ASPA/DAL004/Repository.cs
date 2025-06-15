using System.Numerics;
using System.Text.Json;
using System.Xml;

namespace DAL004
{
    public class Repository : IRepository, IDisposable
    {
        public static string JSONFileName = string.Empty;
        private bool disposedValue;
        private List<Celebrity>? celebrityList;
        int changedCount = 0;

        public string BasePath { get; private set; } = null!;

        public static Repository Create(string basePath)
        {
            var r = new Repository() { BasePath = basePath };
            r.LoadCelebrities();
            return r;
        }

        // Метод без проверки существования файла/каталога.
        private void LoadCelebrities()
        {
            // Сразу пытаемся считать содержимое файла.
            string content = File.ReadAllText(Path.Combine(BasePath, JSONFileName!));
            celebrityList = JsonSerializer.Deserialize<List<Celebrity>>(content);
        }

        private bool SaveCelebrities()
        {
            var json = JsonSerializer.Serialize(celebrityList);
            if (json == null)
            {
                return false;
            }
            File.WriteAllText(Path.Combine(BasePath, JSONFileName!), json);
            return true;
        }

        public Celebrity[] getAllCelebrities()
        {
            // Всегда читаем данные из файла
            LoadCelebrities();
            return celebrityList?.ToArray() ?? new Celebrity[0];
        }

        public Celebrity[] getCelebritiesBySurname(string Surname)
        {
            LoadCelebrities();
            return celebrityList?.Where(c => c.Surname == Surname).ToArray() ?? new Celebrity[0];
        }

        public Celebrity? getCelebrityById(int id)
        {
            LoadCelebrities();
            return celebrityList?.FirstOrDefault(c => c.Id == id);
        }

        public string? getPhotoPathById(int id)
        {
            LoadCelebrities();
            return celebrityList?.FirstOrDefault(c => c.Id == id)?.PhotoPath;
        }

        private int? GetNextID(int originalId)
        {
            // Перед выполнением операции можно также загрузить актуальные данные, если требуется.
            LoadCelebrities();

            if (celebrityList is null)
            {
                return null;
            }

            int id = originalId;
            while (celebrityList.Any(c => c.Id == id))
            {
                id++;
            }

            return id;
        }

        public int? addCelebrity(Celebrity celebrity)
        {
            // Читаем данные перед изменением
            LoadCelebrities();
            var id = GetNextID(celebrity.Id);

            if (celebrityList is null || id is null)
            {
                return null;
            }

            Celebrity celebrity1 = new Celebrity(id.Value, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
            celebrityList.Add(celebrity1);

            changedCount++;
            SaveChanges();
            return id;
        }

        public bool delCelebrityById(int id)
        {
            LoadCelebrities();
            var cel = celebrityList?.FirstOrDefault(c => c.Id == id);

            if (cel is null)
            {
                return false;
            }

            var isDeleted = celebrityList.Remove(cel);

            if (isDeleted)
            {
                changedCount++;
                SaveChanges();
            }


            return isDeleted;
        }

        public int? updCelebrityById(int id, Celebrity celebrity)
        {
            LoadCelebrities();

            if (celebrityList is null)
            {
                return null;
            }

            int instance = celebrityList.FindIndex(c => c.Id == id);
            if (instance == -1)
            {
                return null;
            }

            Celebrity celebrity1 = new Celebrity(id, celebrity.Firstname, celebrity.Surname, celebrity.PhotoPath);
            celebrityList[instance] = celebrity1;

            changedCount++;
            SaveChanges();
            return id;
        }

        public int SaveChanges()
        {
            if (changedCount > 0)
            {
                if (SaveCelebrities())
                {
                    return changedCount;
                }
            }
            return 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // Освобождение управляемых ресурсов
                }
                // Освобождение неуправляемых ресурсов
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
