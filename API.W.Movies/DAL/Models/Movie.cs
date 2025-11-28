using System.ComponentModel.DataAnnotations;

namespace API.W.Movies.DAL.Models
{
    public class Movie : AuditBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public int Duration { get; set; }   // en minutos

        [Required]
        [MaxLength(10)]
        public string Classification { get; set; }

        public string? Description { get; set; }
    }
}

