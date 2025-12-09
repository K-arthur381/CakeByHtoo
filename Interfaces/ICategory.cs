using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    internal interface ICategory
    {
        Task<List<Category>> GetAllCategory();

        Task<Category> GetCategorybyId(int id);

        Task AddCategory(Category category);

        Task UpdateCategory(Category category);

        Task DeleteCategory(int id);
    }
}
