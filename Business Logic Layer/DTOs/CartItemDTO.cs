using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class CartItemDTO
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }     
        public string ProductName { get; set; } = string.Empty;
        public string ProductImages { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string USerName { get; set; } = string.Empty;
       
        public decimal Amount { get; set; }




    }
}
