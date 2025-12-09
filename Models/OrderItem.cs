using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        public byte[]? ImageData { get; set; }
        [Required]
        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }

        [Required]
        public int? ProductItemId { get; set; }

        [ForeignKey(nameof(ProductItemId))]
        public ProductItem? ProductItem { get; set; }

        [Required]
        public int? ProductPriceId { get; set; }

        [ForeignKey(nameof(ProductPriceId))]
        public ProductPrice? ProductPrice { get; set; }

        public int Quantity { get; set; }

        public string? Color { get; set; }

        public string? Accessory { get; set; }

        public string? CakeNote { get; set; }

        public decimal? UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtraPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SubTotal { get; set; }
    }
}
