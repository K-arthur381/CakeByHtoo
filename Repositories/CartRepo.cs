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
    public class CartRepo : ICart
    {
        private readonly CakeByHtooDBContent _context;

        public CartRepo(CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<Cart>> GetAllCarts()
        {
            return await _context.Carts
                .Include(c => c.ProductPrice)
                    .ThenInclude(pp => pp.Product)
                .Include(c => c.ProductPrice.Flavour)
                .ToListAsync();
        }

        public async Task AddToCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
        }
        public async Task<Cart> GetCartById(int cartId)
        {
            return await _context.Carts
                .Include(c => c.ProductPrice)
                .ThenInclude(pp => pp.Product)
                .FirstOrDefaultAsync(c => c.CartId == cartId);
        }
        public async Task UpdateCart(Cart cart)
        {
            var existingCart = await _context.Carts.FindAsync(cart.CartId);
            if (existingCart != null)
            {
                existingCart.Quantity = cart.Quantity;
                existingCart.Color = cart.Color;
                existingCart.Accessory = cart.Accessory;
                existingCart.CakeNote = cart.CakeNote;
                existingCart.ExtraPrice = cart.ExtraPrice;
                existingCart.SubTotal = cart.SubTotal;
                existingCart.ImageData = cart.ImageData;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCart(int cartId)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }


    }
}
