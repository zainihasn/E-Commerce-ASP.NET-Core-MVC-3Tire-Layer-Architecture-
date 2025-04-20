using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Service.Base
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAll();
        Task<CategoryDTO> GetById(int id);
        Task Add(CategoryDTO productDto);
        Task Update(CategoryDTO productDto);
        Task Delete(int id);
    }
}
