using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyRealWorldUnitTest.Web.Controllers;
using UdemyRealWorldUnitTest.Web.Models;

namespace UdemyRealWorldUnitTest.Test
{
    public class ProductsControllerWithSqlServerLocalDb : ProductControllerTest
    {
        public ProductsControllerWithSqlServerLocalDb()
        {
            var sqlCon = @"Server=(localdb)\MSSQLLocalDB;Database=TestDB,TrustedConnection=true,MultipleActieResultSet=true";
            SetContextOptions(new DbContextOptionsBuilder<UdemyUnitTestDbContext>().UseSqlServer(sqlCon).Options);
        }

        [Fact]
        public async Task Create_ModelValidProduct_ReturnRedirectToActionWithProduct()
        {

            var newProduct = new Product { Name = "Kalem", Price = 200, Stock = 4, Color = "kırmızı"};

            using (var context = new UdemyUnitTestDbContext(_contextOptions))
            {
                var category = context.Categories.First();
                newProduct.CategoryId = category.Id;
                var controller = new ProductsController(context);
                var result = await controller.Create(newProduct);
                var redirect = Assert.IsType<RedirectToActionResult>(result);
                Assert.Equal("Index", redirect.ActionName);

            }
            using (var context = new UdemyUnitTestDbContext(_contextOptions))
            {
                var product = context.Products.FirstOrDefault(x => x.Name == newProduct.Name);
                Assert.Equal(newProduct.Name, product.Name);
            }







        }

        [Theory]
        [InlineData(1)]
        public async Task DeleteCategory_ExistCategoryId_DeletedAllProducts(int categoryId)
        {
            using (var context = new UdemyUnitTestDbContext(_contextOptions))
            {
                var category = await context.Categories.FindAsync(categoryId);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            using (var context = new UdemyUnitTestDbContext(_contextOptions))
            {
                var product = await context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
                Assert.Empty(product);
            }

        }

    }
}
