
using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BooksApp.Controllers;

[ApiController]
[Route("api/generos")]
public class GenreController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GenreController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<GenreDTO>>> GetGenres()
    {
        var genres = await _context.Genres.ToListAsync();
        return _mapper.Map<List<GenreDTO>>(genres);
    }

    [HttpGet("{id:int}", Name = "ObtenerGenero")]
    public async Task<ActionResult<List<GenreDTO>>> GetGenreById(int id)
    {

        var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return NotFound();
        }

        var GenreDTO = _mapper.Map<GenreDTO>(genre);
        return Ok(GenreDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CreateGenre([FromBody] CreateGenreDTO CreateGenreDTO)
    {

        var GenreExists = await _context.Genres.AnyAsync(g => g.Name == CreateGenreDTO.Name);

        if (GenreExists)
        {
            return BadRequest();
        }

        var genre = _mapper.Map<Genre>(CreateGenreDTO);


        _context.Add(genre);
        await _context.SaveChangesAsync();

        var genreDTO = _mapper.Map<GenreDTO>(genre);

        return CreatedAtRoute("ObtenerGenero", new { id = genre.Id }, genreDTO);

    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateGenre([FromBody] CreateGenreDTO CreateGenreDTO, int id)
    {
        var GenreExists = await _context.Genres.AnyAsync(g => g.Id == id);

        if (!GenreExists)
        {
            return NotFound();
        }

        var genre = _mapper.Map<Genre>(CreateGenreDTO);
        genre.Id = id;

        _context.Update(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        _context.Remove(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
