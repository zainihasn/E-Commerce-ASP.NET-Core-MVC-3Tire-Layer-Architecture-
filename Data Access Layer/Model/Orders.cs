using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Model
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }   
        public DateTime OrderDate { get; set; }
        [ForeignKey("GuestInfo")]
        public int GuestId { get; set; }
        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public int Status { get; set; }
        public bool IsCancel { get; set; }
        public List<OrderItem> Items { get; set; } = new();        
        public GuestInfo? GuestInfo { get; set; }
        public Payment? Payment { get; set; }


    }

    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; } // FK
     
        public Orders Order { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        [NotMapped]
        public decimal Amount => Quantity * Price;
    }

    public class GuestInfo
    {
        
        [Key]
        public int GuestId { get; set; }
        public string UserName { get; set; } = string.Empty;
        [Required]
        [StringLength(2000)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(2000)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(2000)]
        public string Address { get; set; } = string.Empty;
        public ICollection<Orders> orders { get; set; } = new List<Orders>();

    }
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string CardName { get; set; } = string.Empty;
    }
   

}