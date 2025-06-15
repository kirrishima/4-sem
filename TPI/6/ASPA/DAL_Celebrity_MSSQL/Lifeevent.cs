using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Celebrity_MSSQL
{
    public class Lifeevent  // Событие в жизни знаменитости
    {
        public Lifeevent() { this.Description = string.Empty; }
        public int Id { get; set; }                          // Id События
        public int CelebrityId { get; set; }                 // Id Знаменитости
        public DateTime Date { get; set; }                   // дата События
        public string Description { get; set; }              // описание События
        public string? ReqPhotoPath { get; set; }            // request path Фотографии
        public virtual bool Update(Lifeevent lifeevent)      // --вспомогательный метод
        {
            CelebrityId = lifeevent.CelebrityId;
            Date = lifeevent.Date;
            Description = lifeevent.Description;
            ReqPhotoPath = lifeevent.ReqPhotoPath;

            return true;
        }
    }
}
