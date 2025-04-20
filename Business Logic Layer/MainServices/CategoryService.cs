using AutoMapper;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Service.Base;
using DataAccessLayer.Model;
using DataAccessLayer.Repository.Base;

namespace BusinessLogicLayer.Service
{


    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _MyUnit;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork MyUnit, IMapper mapper)
        {
            _MyUnit = MyUnit;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetAll() => _mapper.Map<List<CategoryDTO>>(await _MyUnit.Category.FindAllAsync());
        public async Task<CategoryDTO> GetById(int CategoryId) => _mapper.Map<CategoryDTO>(await _MyUnit.Category.FindByIdAsync(CategoryId));  
        public async Task Add(CategoryDTO CategoryDTO) => await _MyUnit.Category.AddOneAsync(_mapper.Map<Category>(CategoryDTO));
       

        public async Task Update(CategoryDTO CategoryDTO)
        {
            var existingCategory = await _MyUnit.Category.FindByIdAsync(CategoryDTO.CategoryId);
            if (existingCategory == null) return;
            _mapper.Map(CategoryDTO, existingCategory);
            await _MyUnit.Category.EditOneAsync(existingCategory);
        }

        public async Task Delete(int id)
        {
            var Category = await _MyUnit.Category.FindByIdAsync(id);
            if (Category == null) return;
            await _MyUnit.Category.DeleteOneAsync(Category);
        }
    }
}

