using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogicLayer.DTOs
{
    public class OrdersDTO
    {
        [Key]
        public int OrderId { get; set; }       
        public DateTime OrderDate { get; set; }
        public int? GuestId { get; set; } = null;
        public int PaymentId { get; set; }
        public int Status { get; set; }
        public bool IsCancel { get; set; }
        public List<OrderItemDTO> Items { get; set; } = new();
        
        [ForeignKey("GuestId")]
        public GuestInfoDTO? GuestInfo { get; set; }

        [ForeignKey("PaymentId")]
        public PaymentDTO? Payment { get; set; }
    }

    public class OrderItemDTO
    {
        [Key]
        public int OrderItemId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; } // FK

        public OrdersDTO Order { get; set; }  // Navigation Property
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [NotMapped]
        public decimal Amount => Quantity * Price;
    }

    public class GuestInfoDTO
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

        public ICollection<OrdersDTO> orders { get; set; } = new List<OrdersDTO>();

       
    }
    public class PaymentDTO {
        [Key]
        public int PaymentId { get; set; }
        public string CardName { get; set; } = string.Empty;
    }


}

