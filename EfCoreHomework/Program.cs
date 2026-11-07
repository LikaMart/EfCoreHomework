using EfCoreHomework.Data;
using Microsoft.EntityFrameworkCore;

var db = new AppDbContext();

Console.WriteLine("EF Core Homework");

Console.WriteLine("-- Task 1.1 --");

var products = db.Products
    .Where(p => p.Price > 600)
    .OrderByDescending(p => p.Price)
    .ToList();

foreach (var p in products)
{
    Console.WriteLine($"{p.Name} - {p.Price}");
}


Console.WriteLine();
Console.WriteLine("-- Task 1.2 --");

var productInfo = db.Products
    .Select(p => new
    {
        p.Name,
        p.Price
    })
    .ToList();

foreach (var p in productInfo)
{
    Console.WriteLine($"{p.Name} - {p.Price}");
}


Console.WriteLine();
Console.WriteLine("-- Task 1.3 --");

var joinResult =
    from p in db.Products
    join c in db.Categories
        on p.CategoryId equals c.Id
        into grp
    from c in grp.DefaultIfEmpty()
    select new
    {
        ProductName = p.Name,
        CategoryName = c == null ? "NULL" : c.Name
    };

foreach (var item in joinResult)
{
    Console.WriteLine(
        $"{item.ProductName} - {item.CategoryName}");
}


Console.WriteLine();
Console.WriteLine("-- Task 1.4 --");

var groupResult = db.Products
    .GroupBy(p => p.CategoryId)
    .Select(g => new
    {
        CategoryId = g.Key,
        Count = g.Count()
    })
    .ToList();

foreach (var item in groupResult)
{
    Console.WriteLine(
        $"CategoryId: {item.CategoryId}, Count: {item.Count}");
}


Console.WriteLine();
Console.WriteLine("-- Task 1.5 --");

var average = db.Products.Average(p => p.Price);
var sum = db.Products.Sum(p => p.Price);
var max = db.Products.Max(p => p.Price);

Console.WriteLine($"Average: {average}");
Console.WriteLine($"Sum: {sum}");
Console.WriteLine($"Max: {max}");


Console.WriteLine();
Console.WriteLine("-- Task 1.6 --");

var leftJoinResult =
    db.Categories
      .GroupJoin(
            db.Products,
            c => c.Id,
            p => p.CategoryId,
            (c, products) => new
            {
                Category = c.Name,
                ProductsCount = products.Count()
            })
      .ToList();

foreach (var item in leftJoinResult)
{
    Console.WriteLine(
        $"{item.Category} -> {item.ProductsCount}");
}


Console.WriteLine();
Console.WriteLine("-- Task 2.1 --");

var categories =
    db.Categories
      .Include(c => c.Products)
      .ToList();

foreach (var category in categories)
{
    Console.WriteLine(category.Name);

    foreach (var product in category.Products)
    {
        Console.WriteLine($"   {product.Name}");
    }
}


Console.WriteLine();
Console.WriteLine("-- Task 2.2 --");

var filteredCategories =
    db.Categories
      .Include(c =>
            c.Products.Where(p => p.Price > 600))
      .ToList();

foreach (var category in filteredCategories)
{
    Console.WriteLine(category.Name);

    foreach (var product in category.Products)
    {
        Console.WriteLine($"   {product.Name}");
    }
}


Console.WriteLine();
Console.WriteLine("-- Task 3.1 --");

decimal min = 600;

var sqlProducts = db.Products
    .FromSqlInterpolated(
        $"SELECT * FROM Products WHERE Price > {min}")
    .ToList();

foreach (var p in sqlProducts)
{
    Console.WriteLine($"{p.Name} - {p.Price}");
}


Console.WriteLine();
Console.WriteLine("-- Task 3.2 --");

var sqlLinqProducts = db.Products
    .FromSqlInterpolated(
        $"SELECT * FROM Products WHERE Price > {min}")
    .OrderBy(p => p.Name)
    .ToList();

foreach (var p in sqlLinqProducts)
{
    Console.WriteLine($"{p.Name} - {p.Price}");
}

Console.WriteLine();
Console.WriteLine("-- Task 3.3 --");

db.Database.ExecuteSqlRaw(
    @"UPDATE Products
      SET Price = Price * 1.10");

Console.WriteLine("Prices increased by 10%");


Console.WriteLine();
Console.WriteLine("-- Bonus --");

db.Database.ExecuteSqlRaw(
    @"UPDATE Products
      SET Price = Price * 1.05
      WHERE Price > 100");

Console.WriteLine("Prices >100 increased by 5%");


Console.WriteLine();
Console.WriteLine("All Tasks Completed!");