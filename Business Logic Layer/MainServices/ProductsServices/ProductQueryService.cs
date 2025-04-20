using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.ProductsServices
{
    public class ProductQueryService : IProductQueryService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ProductQueryService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetByCategoryAsync(int categoryId , int? take)
        {
            var data = await _unit.Product.FindAllAsync(p => p.CategoryId == categoryId, take: take);
            return _mapper.Map<List<ProductDTO>>(data);
        }

        public async Task<ProductDTO> GetDetailsByIdAsync(int id)
        {
            var data = await _unit.ProImaRep.GetProductRatingById(id);
            var dto = _mapper.Map<ProductDTO>(data);
            dto.AverageRating = dto.ProductReview?.Any() == true
                ? dto.ProductReview.Average(r => r.Rating)
                : 0;
            return dto;
        }
       
        public async Task<List<ProductDTO>> SearchAsync(string keyword) =>
            _mapper.Map<List<ProductDTO>>(await _unit.ProImaRep.GetProductsBySearchAsync(keyword));

        public async Task<List<ProductDTO>> GetTopSellingAsync(int count) =>
            _mapper.Map<List<ProductDTO>>(await _unit.ProImaRep.GetTopSellingProductsAsync(count));

        public async Task<List<ProductDTO>> GetLowStockProductsAsync(int threshold) =>
            _mapper.Map<List<ProductDTO>>(await _unit.ProImaRep.GetLowStockProductsAsync(threshold));
        
    }

}
