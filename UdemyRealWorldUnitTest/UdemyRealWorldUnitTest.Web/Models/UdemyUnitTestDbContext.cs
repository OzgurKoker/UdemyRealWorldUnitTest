using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UdemyRealWorldUnitTest.Web.Models;

public partial class UdemyUnitTestDbContext : DbContext
{
    public UdemyUnitTestDbContext()
    {
    }

    public UdemyUnitTestDbContext(DbContextOptions<UdemyUnitTestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });
        //modelBuilder.Entity<Category>().HasData(new Category
        //{ Id = 1, Name = "Kalemler" },
        //new Category { Id = 2, Name = "Defterler" }
        //);






        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
