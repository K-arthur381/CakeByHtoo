using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class OrderData
    {
        public string storeName { get; set; }
        public int orderId { get; set; }
        public DateTime? orderDate { get; set; }
        public DateTime? pickupDate { get; set; }
        public string orderNote { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string customerAddress { get; set; }
        public decimal totalAmount { get; set; }
        public List<OrderItemData> orderItems { get; set; }
    }

    public class OrderItemData
    {
        public string productItemName { get; set; }
        public string categoryName { get; set; }
        public int? categoryId { get; set; }
        public string productSize { get; set; }
        public string flavourName { get; set; }
        public string accessory { get; set; }
        public string cakeNote { get; set; }
        public int quantity { get; set; }
        public decimal unitPrice { get; set; }
        public decimal extraPrice { get; set; }
    }


}
