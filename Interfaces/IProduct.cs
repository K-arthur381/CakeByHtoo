using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
        Task<List<Product>> GetProductsByCategory(int id);
        Task<List<Category>> GetCategories();
        Task<List<ProductSize>> GetProductSizes();
        Task<List<Flavour>> GetFlavours();
    }
}
