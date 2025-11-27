using System.ComponentModel.DataAnnotations;

namespace API.W.Movies.DAL.Models.Dtos
{
    public class CategoryCreateUpdateDto
    {
        [Required(ErrorMessage = "El nombre de la categoria obligatoria")]
        [MaxLength(100, ErrorMessage = "El número máximo es de 100 caracteres")]
        public string Name { get; set; }

    }
}
