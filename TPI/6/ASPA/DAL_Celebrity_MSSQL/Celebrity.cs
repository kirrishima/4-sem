using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_Celebrity_MSSQL
{
    public class Celebrity  // Знаменитость
    {
        public Celebrity()
        {
            this.FullName = string.Empty;
            this.Nationality = string.Empty;
        }

        public int Id { get; set; }                          // Id Знаменитости
        public string FullName { get; set; }                 // полное имя Знаменитости
        public string Nationality { get; set; }              // гражданство Знаменитости (2 символа ISO)
        public string? ReqPhotoPath { get; set; }            // request path Фотографии
        public virtual bool Update(Celebrity celebrity)      // --вспомогательный метод
        {
            FullName = celebrity.FullName;
            Nationality = celebrity.Nationality.Substring(0, 2);
            ReqPhotoPath = celebrity.ReqPhotoPath;

            return true;
        }
    }
}
