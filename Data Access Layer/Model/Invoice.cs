using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class Invoice
    {

        [Key]
        public int InvoiceId { get; set; }
        [Required]
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        [MaxLength(100)]
        public string? CustomerName { get; set; }
        [MaxLength(100)]
        public string? CustomerEmail { get; set; }
        [MaxLength(20)]
        public string? CustomerPhone { get; set; }
        [Required]
        [StringLength(2000)]
        public string Address { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? PaymentMethod { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }


        [ForeignKey("OrderId")]
        public Orders? Order { get; set; }

 
        public List<InvoiceItem> Items { get; set; } = new();
    }
        
    public class InvoiceItem
    {
        [Key]
        public int InvoiceItemId { get; set; }
        [Required]
        public int InvoiceId { get; set; }    
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        [NotMapped]
        public decimal Amount => Quantity * UnitPrice;
        [ForeignKey("InvoiceId")]
        public Invoice? Invoice { get; set; }
    }
}

