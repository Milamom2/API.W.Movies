using API.W.Movies.DAL.Models;
using API.W.Movies.DAL.Models.Dtos;
using API.W.Movies.Repository;
using API.W.Movies.Services.IServices;
using AutoMapper;

namespace API.W.Movies.Services
{
    
    public class CategoryService: ICategoryService
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
            throw new NotImplementedException();
        }

        public Task<bool> CategoryExistsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            //Validar si la categoria ya existe
            var categoryExists = await _categoryRepository.CategoryExistsByNameAsync(categoryCreateDto.Name);

            if (categoryExists)
            {
                throw new InvalidOperationException($"Ya existe una categoría con el nombre de '{categoryCreateDto.Name}'");
            }

            //Mapear el DTO a la entidad
            var category = _mapper.Map<Category>(categoryCreateDto);

            //Crear la categoria en el repositorio
            var categoryCreated = await _categoryRepository.CreateCategoryAsync(category);

            if (!categoryCreated)
            {
                throw new Exception("Ocurrió un error al crear la categoría.");
            }

            //Mapear la entidad creada a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _categoryRepository.GetCategoriesAsync(); //Llamando el metodo desde la capa de repositorio
            
            return _mapper.Map<ICollection<CategoryDto>>(categories); //Mapeando la entidad a DTO
            
        }

        public async Task<CategoryDto> GetCategoryAsync(int id)
        {
            //Obtengo la categoria del repositorio
            var category = await _categoryRepository.GetCategoryAsync(id); 

            //Mapeando la entidad a DTO
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> UpdateCategoryAsync(int id, Category categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
