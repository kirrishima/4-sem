using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Celebrity_MSSQL
{
    public interface IRepository : IRepository<Celebrity, Lifeevent> { }

    public class Repository : IRepository
    {
        private Context context;
        public Repository()
        {
            context = new Context();
        }
        public Repository(string connectionString)
        {
            context = new Context(connectionString);
        }
        public IRepository Create()
        {
            return new Repository();
        }
        public static IRepository Create(string connectionString)
        {
            return new Repository(connectionString);
        }

        public bool AddCelebrity(Celebrity celebrity)
        {
            context.Celebrities.Add(celebrity);
            return context.SaveChanges() > 0;
        }

        public bool AddLifeevent(Lifeevent lifeevent)
        {
            //var existing = context.Lifeevents.Where(e =>
            //   e.CelebrityId == lifeevent.CelebrityId &&
            //   e.Date == lifeevent.Date &&
            //   e.Description == lifeevent.Description &&
            //   e.ReqPhotoPath == lifeevent.ReqPhotoPath);

            //if (!existing.Any())
            //{
            context.Lifeevents.Add(lifeevent);
            return context.SaveChanges() > 0;
            //}
            //else
            //{
            //    lifeevent.Id = existing.First().Id;
            //}

            //return true;
        }

        public bool DelCelebrity(int id)
        {
            var celebrity = context.Celebrities.Find(id);
            if (celebrity != null)
            {
                context.Celebrities.Remove(celebrity);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DelLifeevent(int id)
        {
            var lifeevent = context.Lifeevents.Find(id);
            if (lifeevent != null)
            {
                context.Lifeevents.Remove(lifeevent);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Celebrity> GetAllCelebrities()
        {
            return context.Celebrities.ToList();
        }

        public List<Lifeevent> GetAllLifeevents()
        {
            return context.Lifeevents.ToList();
        }

        public Celebrity? GetCelebrityById(int id)
        {
            return context.Celebrities.Find(id);
        }

        public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
        {
            var lifeevent = context.Lifeevents.FirstOrDefault(l => l.Id == lifeeventId);
            if (lifeevent != null)
            {
                return context.Celebrities.Find(lifeevent.CelebrityId);
            }
            return null;
        }

        public int GetCelebrityIdByName(string name)
        {
            var celebrity = context.Celebrities.FirstOrDefault(c => c.FullName.Contains(name));
            return celebrity != null ? celebrity.Id : -1;
        }

        public Lifeevent? GetLifeeventById(int id)
        {
            return context.Lifeevents.Find(id);
        }

        public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId)
        {
            return context.Lifeevents.Where(l => l.CelebrityId == celebrityId).ToList();
        }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            var existing = context.Celebrities.Find(id);
            if (existing != null)
            {
                // Обновляем необходимые поля
                existing.Update(celebrity);
                context.Entry(existing).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdLifeevent(int id, Lifeevent lifeevent)
        {
            var existing = context.Lifeevents.Find(id);
            if (existing != null)
            {
                existing.Update(lifeevent);
                context.Entry(existing).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }

}
