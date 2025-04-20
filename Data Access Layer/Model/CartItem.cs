using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Model
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductImages { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string UserId { get; set; } = string.Empty;
    }

}
