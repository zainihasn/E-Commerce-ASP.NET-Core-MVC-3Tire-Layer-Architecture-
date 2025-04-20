using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class ProductImages
    {
        [Key]
        public int ImageId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public bool DefaultImage { get; set; } = false;
        public Product? Product { get; set; }
    }
}
