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
    public class ProductItemRepo : IProductItem
    {
        private readonly DBContent.CakeByHtooDBContent _context;

        public ProductItemRepo(DBContent.CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<ProductItem>> GetAllAsync()
        {
            return await _context.ProductItems
                .Include(c=>c.Category)
                .OrderByDescending(p => p.ProductItemId)
                .ToListAsync();
        }

        public async Task<ProductItem> GetByIdAsync(int id)
        {
            return await _context.ProductItems.FindAsync(id);
        }

        public async Task AddAsync(ProductItem item)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Add ProductItem
                _context.ProductItems.Add(item);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine("❌ Error: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("👉 Inner: " + ex.InnerException.Message);
                throw;
            }
        }

        public async Task UpdateAsync(ProductItem item)
        {
            var existingdata = await _context.ProductItems.FindAsync(item.ProductItemId);
            if (existingdata != null)
            {
                existingdata.ProductItemName = item.ProductItemName;
                existingdata.CategoryId = item.CategoryId;
                existingdata.UnitPrice = item.UnitPrice;

                if (item.ImageData != null)
                    existingdata.ImageData = item.ImageData;

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteAsync(int id)
        {
            var item = await _context.ProductItems.FindAsync(id);
            if (item != null)
            {
                _context.ProductItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> GetByNameAsync(string name)
        {
            return await _context.ProductItems
                .AnyAsync(a => a.ProductItemName.ToLower() == name.ToLower());
        }

    }
}
