using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EF_Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Читаем строку подключения из App.config по ключу "DefaultConnection"
            optionsBuilder.UseSqlServer(
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
    }
}
