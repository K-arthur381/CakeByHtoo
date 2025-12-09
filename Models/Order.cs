using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        [Required]
        public string CustomerAddress { get; set; }

        public string? OrderNote { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? PickupDate { get; set; }

        public string PaymentType { get; set; }    // e.g., halfpaid, Postpaid,fullpaid

        public string DeliveryMethod { get; set; } // e.g., Pickup, Delivery

        public decimal? PrepaidAmount { get; set; }

        public decimal? RemainingAmount { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public OrderStatus Status { get; set; }   // Use enum here

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
