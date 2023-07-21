using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Models;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductControllerTest
    {
        protected DbContextOptions<UdemyUnitTestDbContext> _contextOptions { get; private set; }
        public void SetContextOptions(DbContextOptions<UdemyUnitTestDbContext> contextOptions)
        {
            _contextOptions = contextOptions;
            Seed();
        }
        public void Seed()
        {
            using (UdemyUnitTestDbContext context = new UdemyUnitTestDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Categories.Add(new Category { Name = "Kalemler" });
                context.Categories.Add(new Category { Name = "Defterler" });
                context.SaveChanges();

                context.Products.Add(new Product { CategoryId = 1, Name = "Kalem 10", Price = 100, Stock = 100, Color = "kırmızı" });
                context.Products.Add(new Product { CategoryId = 1, Name = "Kalem 10", Price = 100, Stock = 100, Color = "kırmızı" });
                context.SaveChanges();

            }
        }
    }
}
