using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CakeByHtoo.Models;
using System.IO;

namespace CakeByHtoo.DBContent
{
    public class CakeByHtooDBContent : DbContext
    {
        public string DbPath { get; }

        public CakeByHtooDBContent()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"BakeryManagementSystem_Of_CakeByHtoo_DatabaseFolder16");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            DbPath = Path.Combine(folder, "Cakebyhtoo.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        // DbSets for all tables
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Flavour> Flavours { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "🎂Cake" },
                  new Category { CategoryId = 2, Name = "🍦Ice-Cream" },
                 new Category { CategoryId = 3, Name = "🍮Dessert" },
                  new Category { CategoryId = 4, Name = "🥤Juices" }
            );

            // Seed Product Sizes
            modelBuilder.Entity<ProductSize>().HasData(
                    // Cake sizes
                    new ProductSize { ProductSizeId = 1, Size = "6", CategoryId = 1 },
                    new ProductSize { ProductSizeId = 2, Size = "8", CategoryId = 1 },
                    new ProductSize { ProductSizeId = 3, Size = "10", CategoryId = 1 },
                    new ProductSize { ProductSizeId = 4, Size = "11", CategoryId = 1 },
                    new ProductSize { ProductSizeId = 5, Size = "12", CategoryId = 1 },

                    // Ice-Cream sizes
                    new ProductSize { ProductSizeId = 6, Size = "10", CategoryId = 2 },
                    new ProductSize { ProductSizeId = 7, Size = "30", CategoryId = 2 },
                    new ProductSize { ProductSizeId = 8, Size = "50", CategoryId = 2 },
                    new ProductSize { ProductSizeId = 9, Size = "100", CategoryId = 2 },

                    // Cake Dessert sizes
                    new ProductSize { ProductSizeId = 10, Size = "-", CategoryId = 3 },

                    // Cake Slice Juices
                    new ProductSize { ProductSizeId = 11, Size = "-", CategoryId = 4 }
            );


            // Seed Flavours
            modelBuilder.Entity<Flavour>().HasData(
                  new Flavour { FlavourId = 1, Name = "Chocolate" },
                  new Flavour { FlavourId = 2, Name = "Vanilla(Milk)" },
                  new Flavour { FlavourId = 3, Name = "Strawberry" },
                  new Flavour { FlavourId = 4, Name = "Blueberry" },
                  new Flavour { FlavourId = 5, Name = "Rainbow" },
                  new Flavour { FlavourId = 6, Name = "Taro" },
                  new Flavour { FlavourId = 7, Name = "Durian" }
              );


            //Seed Products(Catalog )
            modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, Name = "🎂Chiffon", CategoryId = 1 },
                    new Product { ProductId = 2, Name = "🎂Crepe", CategoryId = 1 },
                     new Product { ProductId = 3, Name = "🍨Ice-Cream_Tub", CategoryId = 2 },
                    new Product { ProductId = 4, Name = "-", CategoryId = 3 },
                    new Product { ProductId = 5, Name = "-", CategoryId = 4 }
                );

            //Seed Product-Prices
            modelBuilder.Entity<ProductPrice>().HasData(
                // ========For Cake===============
                // Chiffon - Chocolate (1) [different prices]
                new ProductPrice { ProductPriceId = 13, ProductId = 1, ProductSizeId = 1, FlavourId = 1, UnitPrice = 30000m },
                new ProductPrice { ProductPriceId = 14, ProductId = 1, ProductSizeId = 2, FlavourId = 1, UnitPrice = 45000m },
                new ProductPrice { ProductPriceId = 15, ProductId = 1, ProductSizeId = 3, FlavourId = 1, UnitPrice = 63000m },
                new ProductPrice { ProductPriceId = 16, ProductId = 1, ProductSizeId = 5, FlavourId = 1, UnitPrice = 72000m },

                // Chiffon - Vanilla (2)
                new ProductPrice { ProductPriceId = 1, ProductId = 1, ProductSizeId = 1, FlavourId = 2, UnitPrice = 25000m },
                new ProductPrice { ProductPriceId = 2, ProductId = 1, ProductSizeId = 2, FlavourId = 2, UnitPrice = 40000m },
                new ProductPrice { ProductPriceId = 3, ProductId = 1, ProductSizeId = 3, FlavourId = 2, UnitPrice = 60000m },
                new ProductPrice { ProductPriceId = 4, ProductId = 1, ProductSizeId = 5, FlavourId = 2, UnitPrice = 70000m },

                // Chiffon - Strawberry (3)
                new ProductPrice { ProductPriceId = 5, ProductId = 1, ProductSizeId = 1, FlavourId = 3, UnitPrice = 25000m },
                new ProductPrice { ProductPriceId = 6, ProductId = 1, ProductSizeId = 2, FlavourId = 3, UnitPrice = 40000m },
                new ProductPrice { ProductPriceId = 7, ProductId = 1, ProductSizeId = 3, FlavourId = 3, UnitPrice = 60000m },
                new ProductPrice { ProductPriceId = 8, ProductId = 1, ProductSizeId = 5, FlavourId = 3, UnitPrice = 70000m },

                // Chiffon - Blueberry (4)
                new ProductPrice { ProductPriceId = 9, ProductId = 1, ProductSizeId = 1, FlavourId = 4, UnitPrice = 25000m },
                new ProductPrice { ProductPriceId = 10, ProductId = 1, ProductSizeId = 2, FlavourId = 4, UnitPrice = 40000m },
                new ProductPrice { ProductPriceId = 11, ProductId = 1, ProductSizeId = 3, FlavourId = 4, UnitPrice = 60000m },
                new ProductPrice { ProductPriceId = 12, ProductId = 1, ProductSizeId = 5, FlavourId = 4, UnitPrice = 70000m },


                // =======================
                // Crepe - Rainbow (5)
                // =======================
                new ProductPrice { ProductPriceId = 17, ProductId = 2, ProductSizeId = 1, FlavourId = 5, UnitPrice = 40000m },
                new ProductPrice { ProductPriceId = 18, ProductId = 2, ProductSizeId = 2, FlavourId = 5, UnitPrice = 55000m },
                new ProductPrice { ProductPriceId = 19, ProductId = 2, ProductSizeId = 4, FlavourId = 5, UnitPrice = 75000m },

                // Crepe - Strawberry (3)
                new ProductPrice { ProductPriceId = 20, ProductId = 2, ProductSizeId = 1, FlavourId = 3, UnitPrice = 40000m },
                new ProductPrice { ProductPriceId = 21, ProductId = 2, ProductSizeId = 2, FlavourId = 3, UnitPrice = 55000m },
                new ProductPrice { ProductPriceId = 22, ProductId = 2, ProductSizeId = 4, FlavourId = 3, UnitPrice = 75000m },
            // =======================
            // Ice-Cream
            // =======================
            // Vinilla
            new ProductPrice { ProductPriceId = 23, ProductId = 3, ProductSizeId = 6, FlavourId = 1, UnitPrice = 15000m },
            new ProductPrice { ProductPriceId = 24, ProductId = 3, ProductSizeId = 7, FlavourId = 1, UnitPrice = 40000m },
            new ProductPrice { ProductPriceId = 25, ProductId = 3, ProductSizeId = 8, FlavourId = 1, UnitPrice = 60000m },
            new ProductPrice { ProductPriceId = 26, ProductId = 3, ProductSizeId = 9, FlavourId = 1, UnitPrice = 110000m },

            // Chocolate
            new ProductPrice { ProductPriceId = 27, ProductId = 3, ProductSizeId = 6, FlavourId = 2, UnitPrice = 15000m },
            new ProductPrice { ProductPriceId = 28, ProductId = 3, ProductSizeId = 7, FlavourId = 2, UnitPrice = 40000m },
            new ProductPrice { ProductPriceId = 29, ProductId = 3, ProductSizeId = 8, FlavourId = 2, UnitPrice = 60000m },
            new ProductPrice { ProductPriceId = 30, ProductId = 3, ProductSizeId = 9, FlavourId = 2, UnitPrice = 110000m },

            // Strawberry
            new ProductPrice { ProductPriceId = 31, ProductId = 3, ProductSizeId = 6, FlavourId = 3, UnitPrice = 15000m },
            new ProductPrice { ProductPriceId = 32, ProductId = 3, ProductSizeId = 7, FlavourId = 3, UnitPrice = 40000m },
            new ProductPrice { ProductPriceId = 33, ProductId = 3, ProductSizeId = 8, FlavourId = 3, UnitPrice = 60000m },
            new ProductPrice { ProductPriceId = 34, ProductId = 3, ProductSizeId = 9, FlavourId = 3, UnitPrice = 110000m },

            // Taro
            new ProductPrice { ProductPriceId = 35, ProductId = 3, ProductSizeId = 6, FlavourId = 6, UnitPrice = 15000m },
            new ProductPrice { ProductPriceId = 36, ProductId = 3, ProductSizeId = 7, FlavourId = 6, UnitPrice = 40000m },
            new ProductPrice { ProductPriceId = 37, ProductId = 3, ProductSizeId = 8, FlavourId = 6, UnitPrice = 60000m },
            new ProductPrice { ProductPriceId = 38, ProductId = 3, ProductSizeId = 9, FlavourId = 6, UnitPrice = 110000m },

            // Durian
            new ProductPrice { ProductPriceId = 39, ProductId = 3, ProductSizeId = 6, FlavourId = 7, UnitPrice = 15000m },
            new ProductPrice { ProductPriceId = 40, ProductId = 3, ProductSizeId = 7, FlavourId = 7, UnitPrice = 40000m },
            new ProductPrice { ProductPriceId = 41, ProductId = 3, ProductSizeId = 8, FlavourId = 7, UnitPrice = 60000m },
            new ProductPrice { ProductPriceId = 42, ProductId = 3, ProductSizeId = 9, FlavourId = 7, UnitPrice = 110000m },

                 // ========For Dessert ===============
                 new ProductPrice { ProductPriceId = 43, ProductId = 4, ProductSizeId = 10, FlavourId = 1, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 44, ProductId = 4, ProductSizeId = 10, FlavourId = 2, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 45, ProductId = 4, ProductSizeId = 10, FlavourId = 3, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 46, ProductId = 4, ProductSizeId = 10, FlavourId = 4, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 47, ProductId = 4, ProductSizeId = 10, FlavourId = 5, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 48, ProductId = 4, ProductSizeId = 10, FlavourId = 6, UnitPrice = 1m },
                  new ProductPrice { ProductPriceId = 49, ProductId = 4, ProductSizeId = 10, FlavourId = 7, UnitPrice = 1m },

                  // ========For Juices ===============
                  new ProductPrice { ProductPriceId = 50, ProductId = 5, ProductSizeId = 11, FlavourId = 1, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 51, ProductId = 5, ProductSizeId = 11, FlavourId = 2, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 52, ProductId = 5, ProductSizeId = 11, FlavourId = 3, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 53, ProductId = 5, ProductSizeId = 11, FlavourId = 4, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 54, ProductId = 5, ProductSizeId = 11, FlavourId = 5, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 55, ProductId = 5, ProductSizeId = 11, FlavourId = 6, UnitPrice = 1m },
                   new ProductPrice { ProductPriceId = 56, ProductId = 5, ProductSizeId = 11, FlavourId = 7, UnitPrice = 1m }
                );

        }



        public void SeedProductItems()
        {
            // Seed Product-items with embedded images
            if (!ProductItems.Any())
            {
                // Use project folder relative path
                //var basePath = Path.Combine(Environment.CurrentDirectory, "Resources", "Images");
                var basePath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "Resources", "Images");

                if (basePath != null) { 

                var images = new[]
                {
                     new { Name = "Custom-Cake", FileName = "customcake.png" },
                    new { Name = "CakeDesign-1", FileName = "product1.jpg" },
                    new { Name = "CakeDesign-2", FileName = "product2.jpg" },
                    new { Name = "CakeDesign-3", FileName = "product3.jpg" },
                    new { Name = "CakeDesign-4", FileName = "product4.jpg" },
                    new { Name = "CakeDesign-5", FileName = "product5.jpg" },
                    new { Name = "CakeDesign-6", FileName = "product6.jpg" },
                    new { Name = "CakeDesign-7", FileName = "product7.jpg" },
                    new { Name = "CakeDesign-8", FileName = "product8.jpg" },
                    new { Name = "CakeDesign-9", FileName = "product9.jpg" },
                    new { Name = "CakeDesign-10", FileName = "product10.jpg" },
                    new { Name = "CakeDesign-11", FileName = "product11.jpg" },
                    new { Name = "CakeDesign-12", FileName = "product12.jpg" },
                    new { Name = "CakeDesign-13", FileName = "product13.jpg" },
                    new { Name = "CakeDesign-14", FileName = "product14.jpg" },
                    new { Name = "CakeDesign-15", FileName = "product15.jpg" },
                    new { Name = "CakeDesign-16", FileName = "product16.jpg" },
                    new { Name = "CakeDesign-17", FileName = "product17.jpg" },
                    new { Name = "CakeDesign-18", FileName = "product18.jpg" },
                    new { Name = "CakeDesign-19", FileName = "product19.jpg" },
                    new { Name = "CakeDesign-20", FileName = "product20.jpg" },
                    new { Name = "CakeDesign-21", FileName = "product21.jpg" },
                    new { Name = "CakeDesign-22", FileName = "product22.jpg" },
                    new { Name = "CakeDesign-23", FileName = "product23.jpg" },
                    new { Name = "CakeDesign-24", FileName = "product24.jpg" },
                    new { Name = "CakeDesign-25", FileName = "product25.jpg" },
                    new { Name = "CakeDesign-26", FileName = "product26.jpg" },
                    new { Name = "CakeDesign-27", FileName = "product27.jpg" },
                    new { Name = "CakeDesign-28", FileName = "product28.jpg" },
                    new { Name = "CakeDesign-29", FileName = "product29.jpg" },
                     new { Name = "Ice-Cream Tub", FileName = "ice_cream1.jpg" },
                     new { Name = "Strawberry Crepe Cake Slice", FileName = "slice1.jpg" },
                     new { Name = "Rainbow Crepe Cake Slice", FileName = "slice2.jpg" },
                     new { Name = "Blueberry Chiffon Cake Slice", FileName = "slice3.jpg" },
                     new { Name = "Chocolate Chiffon Cake Slice", FileName = "slice4.jpg" },
                     new { Name = "Original-Pudding", FileName = "desert1.jpg" },
                     new { Name = "CheeseJelly-Pudding", FileName = "desert2.jpg" },
                       new { Name = "BubbleTea", FileName = "bubble.jpg" },
                        new { Name = "Thai-MilkTea", FileName = "thaimilktea.jpg" }
                            };

                foreach (var item in images)
                {
                    byte[]? imgData = null;
                    var imagePath = Path.Combine(basePath, item.FileName);

                    if (File.Exists(imagePath))
                    {
                        imgData = File.ReadAllBytes(imagePath);
                    }
                    else
                    {
                        Console.WriteLine($"File not found: {imagePath}");
                    }

                    //int categoryId;

                    //if (item.Name.Contains("Ice"))
                    //{
                    //    categoryId = 2; // Ice cream
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}
                    //else if (item.Name.Contains("Chiffon"))
                    //{
                    //    categoryId = 3; // Cake slices should be under cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        UnitPrice = 3500m,
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}
                    //else if (item.Name.Contains("Crepe"))
                    //{
                    //    categoryId = 3; // Cake slices should be under cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        UnitPrice = 5500m,
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}
                    //else if (item.Name.Contains("Original"))
                    //{
                    //    categoryId = 3; // desert should be under cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        UnitPrice = 3500m,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}
                    //else if (item.Name.Contains("CheeseJelly"))
                    //{
                    //    categoryId = 3; // desert should be under cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        UnitPrice = 5000m,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}
                    //else if (item.Name.Contains("Tea"))
                    //{
                    //    categoryId = 4; // juices should be under cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        UnitPrice = 3500m,
                    //        ImageData = imgData // Save relative file path
                    //    });

                    //}
                    //else
                    //{
                    //    categoryId = 1; // Default to cakes
                    //    ProductItems.Add(new ProductItem
                    //    {
                    //        ProductItemName = item.Name,
                    //        CategoryId = categoryId,
                    //        ImageData = imgData // Save relative file path
                    //    });
                    //}

                    // Save Image FilePath only ,not seeddata ,both work on window,phone ,but slow
                    var filePath = Path.Combine("Resources", "Images", item.FileName);

                    int categoryId;

                    if (item.Name.Contains("Ice"))
                    {
                        categoryId = 2; // Ice cream
                        ProductItems.Add(new ProductItem
                        {
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            FilePath = filePath ,// Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                    else if (item.Name.Contains("Chiffon"))
                    {
                        categoryId = 3; // Cake slices should be under cakes
                        ProductItems.Add(new ProductItem
                        {
                            UnitPrice = 3500m,
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            FilePath = filePath, // Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                    else if (item.Name.Contains("Crepe"))
                    {
                        categoryId = 3; // Cake slices should be under cakes
                        ProductItems.Add(new ProductItem
                        {
                            UnitPrice = 5500m,
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            FilePath = filePath,// Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                    else if (item.Name.Contains("Original"))
                    {
                        categoryId = 3; // desert should be under cakes
                        ProductItems.Add(new ProductItem
                        {
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            UnitPrice = 3500m,
                            FilePath = filePath, // Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                    else if (item.Name.Contains("CheeseJelly"))
                    {
                        categoryId = 3; // desert should be under cakes
                        ProductItems.Add(new ProductItem
                        {
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            UnitPrice = 5000m,
                            FilePath = filePath, // Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                    else if (item.Name.Contains("Tea"))
                    {
                        categoryId = 4; // juices should be under cakes
                        ProductItems.Add(new ProductItem
                        {
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            UnitPrice = 3500m,
                            FilePath = filePath, // Save relative file path
                            ImageData = imgData // Save relative file path
                        });

                    }
                    else
                    {
                        categoryId = 1; // Default to cakes
                        ProductItems.Add(new ProductItem
                        {
                            ProductItemName = item.Name,
                            CategoryId = categoryId,
                            FilePath = filePath, // Save relative file path
                            ImageData = imgData // Save relative file path
                        });
                    }
                }

                SaveChanges();

                }
            }
        }

    }

}
