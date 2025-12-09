using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CakeByHtoo.Models
{
    public class ProductItem
    {
        [Key]
        public int ProductItemId { get; set; }

        [Required]
        public string ProductItemName { get; set; }

        public byte[]? ImageData { get; set; } = null;

        public string? FilePath {  get; set; }

        public decimal UnitPrice { get; set; } = 0;

        // New: Category relationship
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }

    }

}
