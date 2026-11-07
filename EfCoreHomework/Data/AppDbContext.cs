using EfCoreHomework.Models;
using Microsoft.EntityFrameworkCore;

namespace EfCoreHomework.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;
              Database=EfCoreHomeworkDb;
              Trusted_Connection=True;
              TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Laptops" },
            new Category { Id = 2, Name = "Phones" },
            new Category { Id = 3, Name = "Tablets" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Laptop A",
                Price = 1500,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Laptop B",
                Price = 900,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Name = "Phone X",
                Price = 700,
                CategoryId = 2
            },
            new Product
            {
                Id = 4,
                Name = "Phone Y",
                Price = 500,
                CategoryId = 2
            },
            new Product
            {
                Id = 5,
                Name = "Cable",
                Price = 10,
                CategoryId = null
            }
        );
    }
}