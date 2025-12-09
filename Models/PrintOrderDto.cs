using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class PrintOrderDto
    {
        public string? StoreName { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public string? OrderNote { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public PrintOrderItemDto[] OrderItems { get; set; } = Array.Empty<PrintOrderItemDto>();
    }

    public class PrintOrderItemDto
    {
        public string ProductItemName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public decimal ProductSize { get; set; }
        public string? FlavourName { get; set; }
        public string? Accessory { get; set; }
        public string? CakeNote { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal ExtraPrice { get; set; }
    }
}
