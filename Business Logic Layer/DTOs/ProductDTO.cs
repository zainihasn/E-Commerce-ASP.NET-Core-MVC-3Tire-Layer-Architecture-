using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Model;
using Microsoft.AspNetCore.Http;


namespace BusinessLogicLayer.DTOs
{
    public class ProductDTO
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
            public CategoryDTO? Category { get; set; }
            [Required]
            [Range(0, int.MaxValue, ErrorMessage = "لا يمكن أن تكون الكمية سالبة")]
            public int Sold { get; set; } = 0;
            public bool IsActive { get; set; }
            public double AverageRating { get; set; }
    
           public ICollection<ProductReviewDTO> ProductReview { get; set; } = new List<ProductReviewDTO>();
           public ICollection<ProductImageDTO> ProductImages { get; set; } = new List<ProductImageDTO>();  
        }

        public class ProductImageDTO
    {
        [Key]
            public int ImageId { get; set; }
            public IFormFile ClientFile { get; set; }
            public string ImageUrl { get; set; } = string.Empty;
            public int ProductId { get; set; }
          }
       public class ProductReviewDTO
         {
           [Key]
           public int ReviewId { get; set; }
           [Required]
           public int Rating { get; set; }
           [Required]
           [StringLength(250)]
           public string Comment { get; set; } = string.Empty;
           [ForeignKey("Product")]
           public int ProductId { get; set; }
           [ForeignKey("guestInfo")]
           public int GuestId { get; set; }
           public DateTime CreatedDate { get; set; } = DateTime.Today;
           public Product? Product { get; set; }
           public GuestInfo? guestInfo { get; set; }

          }

}


