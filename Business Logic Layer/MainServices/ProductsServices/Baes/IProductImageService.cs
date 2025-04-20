using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.MainServices.ProductsServices.Baes
{
    public interface IProductImageService
    {
        Task<List<ProductImageDTO>> GetAllAsync();
        Task<List<ProductImageDTO>> GetAllByIdAsync(int ImagesId);
        Task<ProductImageDTO> GetByIdAsync(int ImageId);
        Task AddAsync(ProductImageDTO ImageDto);
        Task UpdateAsync(ProductImageDTO ImageDto);
        Task DeleteAsync(int ImageId);
    }

}
