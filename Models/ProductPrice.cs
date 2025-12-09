using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class ProductPrice
    {
        [Key]
        public int ProductPriceId { get; set; }

       
        public int? ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; }
   
        public int? ProductSizeId { get; set; }

        [ForeignKey(nameof(ProductSizeId))]
        public ProductSize? ProductSize { get; set; }

        [Required]
        public int? FlavourId { get; set; }

        [ForeignKey(nameof(FlavourId))]
        public Flavour? Flavour { get; set; }
        public int? ProductItemId { get; set; }
        [ForeignKey(nameof(ProductItemId))]
        public ProductItem? ProductItem { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } = 1;

      //  public ICollection<ProductItem> ProductItem { get; set; } = new List<ProductItem>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
