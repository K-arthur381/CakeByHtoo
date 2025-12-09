using CakeByHtoo.DBContent;
using CakeByHtoo.Interfaces;
using CakeByHtoo.Models;
using Microsoft.EntityFrameworkCore;

namespace CakeByHtoo.Repositories
{
    public class ProductSizeRepo : IProductSize
    {
        private readonly CakeByHtooDBContent _context;

        public ProductSizeRepo(CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<ProductSize>> GetAllAsync()
        {
            return await _context.ProductSizes
                .Include(p => p.Category) // eager load Category
                .AsNoTracking()
                .OrderBy(p => p.Size)     // sort by size
                .ToListAsync();
        }

        public async Task<ProductSize> GetByIdAsync(int id)
        {
            return await _context.ProductSizes
                .Include(p => p.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductSizeId == id);
        }

        public async Task<bool> AddAsync(ProductSize size)
        {
            if (size == null) return false;

            await _context.ProductSizes.AddAsync(size);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(ProductSize size)
        {
            var existingData = await _context.ProductSizes.FindAsync(size.ProductSizeId);
            if (existingData == null) return false;

            existingData.Size = size.Size;
            existingData.CategoryId = size.CategoryId;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var size = await _context.ProductSizes.FindAsync(id);
            if (size == null) return false;

            _context.ProductSizes.Remove(size);
            return await _context.SaveChangesAsync() > 0;
        }

        // 🔎 Extra: Search sizes by category
        public async Task<List<ProductSize>> GetByCategoryAsync(int categoryId)
        {
            return await _context.ProductSizes
                .Where(p => p.CategoryId == categoryId)
                .AsNoTracking()
                .ToListAsync();
        }

       
    }
}
