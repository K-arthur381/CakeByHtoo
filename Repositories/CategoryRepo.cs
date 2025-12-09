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
    internal class CategoryRepo : ICategory
    {
        private readonly CakeByHtooDBContent _dbContext;

        public CategoryRepo(CakeByHtooDBContent dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategorybyId(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }
      

        public async Task  AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            var existingCategory = await _dbContext.Categories.FindAsync(category.CategoryId);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
