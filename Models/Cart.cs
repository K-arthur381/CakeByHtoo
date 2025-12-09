using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class Cart
    {

        [Key]
        public int CartId { get; set; }

        public byte[]? ImageData { get; set; }

        public int? ProductPriceId { get; set; }
      
        [ForeignKey(nameof(ProductPriceId))]
        public ProductPrice? ProductPrice { get; set; }

        [Required]
        public int ProductItemId { get; set; }

        [ForeignKey(nameof(ProductItemId))]
        public ProductItem ProductItem { get; set; }


        public int? ProductSizeId { get; set; }

        [ForeignKey(nameof(ProductSizeId))]
        public ProductSize? ProductSize { get; set; }

      
        public int? FlavourId { get; set; }

        [ForeignKey(nameof(FlavourId))]
        public Flavour? Flavour { get; set; }

        public int Quantity { get; set; }

        public string? Color { get; set; }

        public string? Accessory { get; set; }

        public string? CakeNote { get; set; }

        public decimal? UnitPrice { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ExtraPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SubTotal { get; set; }

      

    }
}
