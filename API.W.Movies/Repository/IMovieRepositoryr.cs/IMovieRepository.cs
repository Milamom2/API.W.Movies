using API.W.Movies.DAL.Models;

namespace API.W.Movies.Repository
{
    public interface IMovieRepository
    {
        Task<ICollection<Movie>> GetMoviesAsync();
        Task<Movie?> GetMovieAsync(int id);
        Task<bool> CreateMovieAsync(Movie movie);
        Task<bool> UpdateMovieAsync(Movie movie);
        Task<bool> DeleteMovieAsync(int id);
        Task<bool> MovieExistsByNameAsync(string name);
        Task<bool> MovieExistsByIdAsync(int id);
    }
}

