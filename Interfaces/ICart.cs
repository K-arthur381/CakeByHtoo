using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    public interface ICart
    {
        Task<List<Cart>> GetAllCarts();
        Task AddToCart(Cart cart);
        Task<Cart> GetCartById(int cartId);
        Task UpdateCart(Cart cart);
        Task DeleteCart(int cartId);

    }
}
