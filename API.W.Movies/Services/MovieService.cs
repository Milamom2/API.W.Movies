using API.W.Movies.DAL.Models;
using API.W.Movies.DAL.Models.Dtos;
using API.W.Movies.Repository;
using API.W.Movies.Services.IServices;
using AutoMapper;

namespace API.W.Movies.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repo;
        private readonly IMapper _mapper;

        public MovieService(IMovieRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ICollection<MovieDto>> GetMoviesAsync()
        {
            var movies = await _repo.GetMoviesAsync();
            return _mapper.Map<ICollection<MovieDto>>(movies);
        }

        public async Task<MovieDto> GetMovieAsync(int id)
        {
            var movie = await _repo.GetMovieAsync(id);

            if (movie == null)
                throw new InvalidOperationException($"No se encontró la película con ID {id}");

            return _mapper.Map<MovieDto>(movie);
        }


        public async Task<MovieDto> CreateMovieAsync(MovieCreateUpdateDto dto)
        {
            if (await _repo.MovieExistsByNameAsync(dto.Name))
                throw new InvalidOperationException("Ya existe una película con ese nombre.");

            var movie = _mapper.Map<Movie>(dto);

            await _repo.CreateMovieAsync(movie);

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<MovieDto> UpdateMovieAsync(MovieCreateUpdateDto dto, int id)
        {
            var movie = await _repo.GetMovieAsync(id);
            if (movie == null)
                throw new InvalidOperationException("No se encontró la película.");

            _mapper.Map(dto, movie);
            await _repo.UpdateMovieAsync(movie);

            return _mapper.Map<MovieDto>(movie);
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            return await _repo.DeleteMovieAsync(id);
        }
    }
}
