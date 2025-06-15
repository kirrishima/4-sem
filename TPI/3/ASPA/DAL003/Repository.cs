using System.Text.Json;

namespace DAL003
{
    public class Repository : IRepository
    {
        public static string? JSONFileName;
        private bool disposedValue;
        private List<Celebrity>? celebrityList;

        public string BasePath { get; private set; } = null!;

        public static Repository Create(string basePath)
        {
            var r = new Repository() { BasePath = basePath };
            r.LoadCelebrities();

            return r;
        }

        private void LoadCelebrities()
        {
            if (!string.IsNullOrWhiteSpace(BasePath) && Directory.Exists(BasePath))
            {
                string content = File.ReadAllText(Path.Combine(BasePath, JSONFileName!));
                celebrityList = JsonSerializer.Deserialize<List<Celebrity>>(content);
            }
        }

        public Celebrity[] getAllCelebrities()
        {
            if (celebrityList == null)
            {
                LoadCelebrities();
            }
            return celebrityList?.ToArray() ?? [];
        }

        public Celebrity[] getCelebritiesBySurname(string Surname)
        {
            if (celebrityList == null)
            {
                LoadCelebrities();
            }
            return celebrityList?.Where(c => c.Surname.ToLower() == Surname.ToLower()).ToArray() ?? [];
        }

        public Celebrity? getCelebrityById(int id)
        {
            if (celebrityList == null)
            {
                LoadCelebrities();
            }
            return celebrityList?.Where(c => c.Id == id).FirstOrDefault();
        }

        public string? getPhotoPathById(int id)
        {
            if (celebrityList == null)
            {
                LoadCelebrities();
            }
            return celebrityList?.Where(c => c.Id == id).FirstOrDefault()?.PhotoPath;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }


}
