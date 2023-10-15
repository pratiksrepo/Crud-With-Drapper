using System.ComponentModel.DataAnnotations;

namespace CrudwithDrapper.Models
{
    public class ProductDto
    {
        [Required,MaxLength(100)]
        public string name { get; set; } = " ";

        [Required, MaxLength(100)]
        public string Brand { get; set; } = " ";

        [Required, MaxLength(100)]
        public string Category { get; set; } = " ";

        [Required]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = " ";
    }
}
