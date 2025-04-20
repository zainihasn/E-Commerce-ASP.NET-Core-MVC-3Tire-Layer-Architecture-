using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.ProductsServices
{
    public class ProductManagementService : IProductManagementService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ProductManagementService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllAsync() =>
            _mapper.Map<List<ProductDTO>>(await _unit.Product.FindAllAsync());

        public async Task<ProductDTO> GetByIdAsync(int id) =>
            _mapper.Map<ProductDTO>(await _unit.Product.FindByIdAsync(id));

        public async Task AddAsync(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unit.Product.AddOneAsync(product);
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {
            var existing = await _unit.Product.FindByIdAsync(productDto.ProductId);
            if (existing == null) return;
            _mapper.Map(productDto, existing);
            await _unit.Product.EditOneAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _unit.Product.FindByIdAsync(id);
            if (product == null) return;
            await _unit.Product.DeleteOneAsync(product);
        }
    }

}
