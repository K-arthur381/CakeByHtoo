using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeByHtoo.Models
{
    public class Flavour
    {
        [Key]
        public int FlavourId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ProductPrice> ProductPrices { get; set; } = new List<ProductPrice>();
    }
}
