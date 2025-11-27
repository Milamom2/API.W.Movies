using API.W.Movies.DAL.Models;
using API.W.Movies.DAL.Models.Dtos;
using API.W.Movies.Repository;
using API.W.Movies.Services.IServices;
using AutoMapper;

namespace API.W.Movies.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> CategoryExistsByIdAsync(int id)
        {
            return await _categoryRepository.CategoryExistsByIdAsync(id);
        }

        public async Task<bool> CategoryExistsByNameAsync(string name)
        {
            return await _categoryRepository.CategoryExistsByNameAsync(name);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateUpdateDto dto)
        {
            var categoryExists = await CategoryExistsByNameAsync(dto.Name);

            if (categoryExists)
                throw new InvalidOperationException($"Ya existe una categoría con el nombre '{dto.Name}'");

            var category = _mapper.Map<Category>(dto);

            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
                throw new Exception("Ocurrió un error al crear la categoría.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            //Verificar si la categoría existe
            var categoryExists = await _categoryRepository.GetCategoryAsync(id);

            if (categoryExists == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID '{id}'");

            //Eliminar la categoría
            var categoryDeleted = await _categoryRepository.DeleteCategoryAsync(id);

            if (!categoryDeleted)
            {
                throw new Exception("Ocurrió un error al eliminar la categoría.");
            }
            return categoryDeleted;
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<ICollection<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);

            if (category == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID '{id}'");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsync(CategoryCreateUpdateDto dto, int id)
        {
            var categoryExists = await _categoryRepository.GetCategoryAsync(id);

            if (categoryExists == null)
                throw new InvalidOperationException($"No se encontró la categoría con ID '{id}'");

            var nameExists = await CategoryExistsByNameAsync(dto.Name);

            if (nameExists && dto.Name != categoryExists.Name)
                throw new InvalidOperationException($"Ya existe una categoría con el nombre '{dto.Name}'");

            _mapper.Map(dto, categoryExists);

            var categoryUpdated = await _categoryRepository.UpdateCategoryAsync(categoryExists);

            if (!categoryUpdated)
                throw new Exception("Ocurrió un error al actualizar la categoría.");

            return _mapper.Map<CategoryDto>(categoryExists);
        }
    }
}
