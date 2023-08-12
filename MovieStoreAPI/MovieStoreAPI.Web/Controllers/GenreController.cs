using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStoreAPI.Business.Services;
using MovieStoreAPI.Data.Entities;

namespace MovieStoreAPI.Web.Controllers;

[Route("api/[controller]s")]
[ApiController]
[Authorize]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;
    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpPost]
    public IActionResult Add(Genre request)
    {
        var result = _genreService.Add(request);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpPost("edit")]
    public IActionResult Edit(Genre request) {
        var result = _genreService.Update(request);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpGet]
    public IActionResult GetAll() { 

        var result = _genreService.List();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id) {
        var result = _genreService.GetById(id);
        if(result is not null)
        {
            return Ok(result);
        }
        return BadRequest("Genre is not found");
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var result = _genreService.Delete(id);
        if (result)
        {
            return Ok(result);
        }
        return BadRequest("Genre is not found");
    }
}
