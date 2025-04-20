using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;

namespace BusinessLogicLayer.DTOs
{
    public class CategoryDTO
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100)]
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryImageUrl { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile ClientFile { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();

    }
}
