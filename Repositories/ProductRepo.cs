using CakeByHtoo.DBContent;
using CakeByHtoo.Interfaces;
using CakeByHtoo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Repositories
{
    public class ProductRepo : IProduct
    {
        private readonly CakeByHtooDBContent _context;
        public ProductRepo(CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductPrices)
                    .ThenInclude(pp => pp.ProductSize)
                .Include(p => p.ProductPrices)
                    .ThenInclude(pp => pp.Flavour)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByCategory(int id)
        {
            return await _context.Products.Where(a=>a.CategoryId==id)
                 .Include(p => p.Category)
                .Include(p => p.ProductPrices)
                    .ThenInclude(pp => pp.ProductSize)
                .Include(p => p.ProductPrices)
                    .ThenInclude(pp => pp.Flavour).ToListAsync();
                 
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductPrices)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(Product product)
        {
            var existing = await _context.Products
                .Include(p => p.ProductPrices)
                .FirstOrDefaultAsync(p => p.ProductId == product.ProductId);

            if (existing != null)
            {
                // Update scalar properties
                existing.Name = product.Name;
                existing.CategoryId = product.CategoryId;
             

                // Remove old prices
                _context.ProductPrices.RemoveRange(existing.ProductPrices);

                // Add new prices with correct ProductId
                foreach (var pp in product.ProductPrices)
                {
                    pp.ProductId = existing.ProductId;  // Make sure foreign key is set
                    _context.ProductPrices.Add(pp);
                }

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductPrices)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product != null)
            {
                _context.ProductPrices.RemoveRange(product.ProductPrices);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Category>> GetCategories() => await _context.Categories.ToListAsync();
        public async Task<List<ProductSize>> GetProductSizes() => await _context.ProductSizes.ToListAsync();
        public async Task<List<Flavour>> GetFlavours() => await _context.Flavours.ToListAsync();
    }

}
