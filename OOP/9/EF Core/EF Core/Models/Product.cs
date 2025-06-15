using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

        // Внешний ключ на Category
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
