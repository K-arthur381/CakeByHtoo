using CakeByHtoo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Interfaces
{
    public interface IOrderRepo
    {
        Task<List<Order>> GetAllOrders();           // Get all orders
        Task<Order> GetOrderById(int orderId);      // Get single order by Id
        Task AddOrder(Order order);                 // Add new order
        Task UpdateOrder(Order order);              // Update existing order
        Task DeleteOrder(int orderId);              // Delete order

        Task<List<Order>> GetAllProgressOrders();
        Task<List<Order>> GetAllCompleteOrders();
        Task<List<Order>> GetAllCancelOrders();
    }
}
