using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    public interface IProductSize
    {
        Task<List<ProductSize>> GetAllAsync();
        Task<ProductSize> GetByIdAsync(int id);
        Task<bool> AddAsync(ProductSize size);
        Task<bool> UpdateAsync(ProductSize size);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductSize>> GetByCategoryAsync(int categoryId);
    }

}
