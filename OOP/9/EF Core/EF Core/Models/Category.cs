using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Навигационное свойство: одна категория — много товаров.
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
