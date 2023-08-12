using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Business.Services;
using MovieStoreAPI.Data.DBContext;
using MovieStoreAPI.Data.Entities;

namespace MovieStoreAPI.Web.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly SimDbContext _dbContext;
        public MovieController(IMovieService movieService, SimDbContext dbContext)
        {
            _movieService = movieService;
            _dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult Add(Movie request)
        {
            var result = _movieService.Add(request);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("edit")]
        public IActionResult Edit(Movie request)
        {
            var result = _movieService.Update(request);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var result = _movieService.List();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _movieService.GetById(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("Movie is not found");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var result = _movieService.Delete(id);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Movie is not found");
        }

        [HttpGet("genres/{movieId}")]
        public IActionResult GetGenresByMovieId(int movieId)
        {
            var genreIds = _movieService.GetGenreByMovieId(movieId);

            if (genreIds == null)
            {
                return BadRequest("Genre is not found");
            }

            var genreNames = _dbContext.Genre
                .Where(g => genreIds.Contains(g.GenreId))
                .Select(g => g.GenreName)
                .ToList();

            return Ok(genreNames);
        }
    }
}
