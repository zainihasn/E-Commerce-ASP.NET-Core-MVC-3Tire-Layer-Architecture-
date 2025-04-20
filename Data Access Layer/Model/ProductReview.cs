using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.X509;

namespace DataAccessLayer.Model
{
    public class ProductReview
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
