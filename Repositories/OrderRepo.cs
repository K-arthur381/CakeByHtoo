using CakeByHtoo.Interfaces;
using CakeByHtoo.Models;
using CakeByHtoo.DBContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CakeByHtoo.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        private readonly CakeByHtooDBContent _context;

        public OrderRepo(CakeByHtooDBContent context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                                 .Include(o => o.OrderItems)
                                 .ThenInclude(oi => oi.ProductPrice)
                                 .ThenInclude(pp => pp.Product)
                                 .OrderByDescending(o=> o.OrderId)
                                 .ToListAsync();
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            //return await _context.Orders
            //            .Include(o => o.OrderItems)
            //                .ThenInclude(oi => oi.ProductPrice)
            //                    .ThenInclude(pp => pp.Product)
            //            .Include(o => o.OrderItems)
            //                .ThenInclude(oi => oi.ProductPrice)
            //                    .ThenInclude(pp => pp.ProductSize)
            //            .Include(o => o.OrderItems)
            //                .ThenInclude(oi => oi.ProductPrice)
            //                    .ThenInclude(pp => pp.Flavour)
            //            .FirstOrDefaultAsync(o => o.OrderId == orderId);

            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }



        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems) // Include related OrderItems
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                // Remove all related OrderItems first
                _context.OrderItems.RemoveRange(order.OrderItems);

                // Then remove the main Order
                _context.Orders.Remove(order);

                await _context.SaveChangesAsync();
            }
        }


        public async Task<List<Order>> GetAllProgressOrders()
        {
            return await _context.Orders
                                .Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.ProductPrice)
                                .ThenInclude(pp => pp.Product)
                                  .Where(o => o.Status != OrderStatus.Complete && o.Status != OrderStatus.Cancel) // use enum
                                .OrderByDescending(o => o.OrderId)
                                .ToListAsync();
        }

        public async Task<List<Order>> GetAllCompleteOrders()
        {
            return await _context.Orders
                              .Include(o => o.OrderItems)
                              .ThenInclude(oi => oi.ProductPrice)
                              .ThenInclude(pp => pp.Product)
                                .Where(o => o.Status == OrderStatus.Complete) // use enum
                              .OrderByDescending(o => o.OrderId)
                              .ToListAsync();
        }
        
              public async Task<List<Order>> GetAllCancelOrders()
        {
            return await _context.Orders
                              .Include(o => o.OrderItems)
                              .ThenInclude(oi => oi.ProductPrice)
                              .ThenInclude(pp => pp.Product)
                                .Where(o => o.Status == OrderStatus.Cancel) // use enum
                              .OrderByDescending(o => o.OrderId)
                              .ToListAsync();
        }
    }
}
