using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class ProductSize
    {
        [Key]
        public int ProductSizeId { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [StringLength(50, ErrorMessage = "Size must be under 50 characters")]
        [Display(Name = "Cake Size")]
        public string Size { get; set; }  // e.g. "6 inches"

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

        public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    }
}
