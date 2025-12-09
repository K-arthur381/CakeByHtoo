using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    internal interface IProductItem
    {
        Task<List<ProductItem>> GetAllAsync();
        Task<bool> GetByNameAsync(string Name);
        Task<ProductItem> GetByIdAsync(int id);
        Task AddAsync(ProductItem item);
        Task UpdateAsync(ProductItem item);
        Task DeleteAsync(int id);

    }
}
