using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryImageUrl { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Today;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }

}
