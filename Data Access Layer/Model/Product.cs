using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProductName { get; set; } = string.Empty;
        [StringLength(500)]
        public string ShortDescription { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(max)")]
        public string LongDescription { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "لا يمكن أن تكون الكمية سالبة")]
        public int Quantity { get; set; }
        [StringLength(30)]
        public string Size { get; set; } = string.Empty;
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; } 
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "لا يمكن أن تكون الكمية سالبة")]
        public int Sold { get; set; } = 0;
        public bool IsCustomized { get; set; } = true;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Today;
        public ICollection<ProductReview> ProductReview { get; set; } = new List<ProductReview>();
        public ICollection<ProductImages> ProductImages { get; set; } = new List<ProductImages>();
    }


}
