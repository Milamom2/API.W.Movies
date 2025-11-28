using API.W.Movies.DAL.Models.Dtos;
using API.W.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.W.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // GET api/movies 
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _service.GetMoviesAsync();
            return Ok(movies);
        }

        // GET api/movies/5
        [HttpGet("{id:int}", Name = "GetMovie")]
        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                var result = await _service.GetMovieAsync(id);
                return Ok(result);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // POST api/movies
        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromBody] MovieCreateUpdateDto dto)
        {
            try
            {
                var created = await _service.CreateMovieAsync(dto);
                return CreatedAtRoute("GetMovie", new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                // Error por duplicado u otra regla de negocio
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Error real del servidor
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        // PUT api/movies/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieCreateUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updated = await _service.UpdateMovieAsync(dto, id);
                return Ok(updated);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/movies/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            return Ok(await _service.DeleteMovieAsync(id));
        }
    }
}
