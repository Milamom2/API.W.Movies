using API.W.Movies.DAL.Models;

namespace API.W.Movies.Repository
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync(); //Me retorna una lista de categorias
       Task<Category?> GetCategoryAsync(int id); //Me retorna una categoria por su id
        Task<bool> CategoryExistsByIdAsync(int id); //Me dice si una categoria existe por su id
        Task<bool> CategoryExistsByNameAsync(string name); //Me dice si una categoria existe por su nombre
        Task<bool> CreateCategoryAsync(Category category); //Me crea una categoria
        Task<bool> UpdateCategoryAsync(Category category); //Me actualiza una categoria --Puedo actualizar el nombre y la fecha de actualizacion
        Task<bool> DeleteCategoryAsync(int id); //Me elimina una categoria por su id
    }
}
