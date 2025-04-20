using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.MainServices.ProductsServices.Baes;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.MainServices.ProductsServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ProductImageService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<List<ProductImageDTO>> GetAllAsync() =>
            _mapper.Map<List<ProductImageDTO>>(await _unit.ProductImages.FindAllAsync());

        public async Task<ProductImageDTO> GetByIdAsync(int ImagesId) =>
            _mapper.Map<ProductImageDTO>(await _unit.ProductImages.SelectOneAsync(i=>i.ProductId== ImagesId));

        public async Task AddAsync(ProductImageDTO dto)
        {
            var entity = _mapper.Map<ProductImages>(dto);
            await _unit.ProductImages.AddOneAsync(entity);
        }

        public async Task UpdateAsync(ProductImageDTO dto)
        {
            var entity = await _unit.ProductImages.FindByIdAsync(dto.ImageId);
            if (entity == null) return;
            _mapper.Map(dto, entity);
            await _unit.ProductImages.EditOneAsync(entity);
        }

        public async Task DeleteAsync(int ImagesId)
        {
            var entity = await _unit.ProductImages.FindByIdAsync(ImagesId);
            if (entity == null) return;
            await _unit.ProductImages.DeleteOneAsync(entity);
        }

        public async Task<List<ProductImageDTO>> GetAllByIdAsync(int ImagesId)
        {
            var images = await  _unit.ProductImages.FindAllAsync(img => img.ProductId == ImagesId);
            return _mapper.Map<List<ProductImageDTO>>(images);
        }




    }

}
