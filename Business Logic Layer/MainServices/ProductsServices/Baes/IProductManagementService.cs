using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.ProductsServices.Baes
{
    public interface IProductManagementService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int ProductId);
        Task AddAsync(ProductDTO productDto);
        Task UpdateAsync(ProductDTO productDto);
        Task DeleteAsync(int ProductId);
    }

}
