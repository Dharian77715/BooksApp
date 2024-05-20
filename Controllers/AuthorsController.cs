using AutoMapper;
using BooksApp;
using BooksApp.DTOs;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Authors.Controllers;

[ApiController]
[Route("api/autores")]
public class AuthorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuthorsController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorsDTO>>> GetAuthors()
    {
        var authors = await _context.Authors.ToListAsync();
        return _mapper.Map<List<AuthorsDTO>>(authors);
    }

    [HttpGet("{id:int}", Name = "ObtenerAutor")]
    public async Task<ActionResult<List<AuthorsDTO>>> GetAuthorById(int id)
    {

        var author = await _context.Authors.SingleOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound($"El autor con el id {id} no fue encontrado");
        }

        var AuthorsDTO = _mapper.Map<AuthorsDTO>(author);
        return Ok(AuthorsDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAuthor([FromBody] CreateAuthorDTO CreateAuthorDTO)
    {

        var authorExists = await _context.Authors.AnyAsync(a => a.Name == CreateAuthorDTO.Name);

        if (authorExists)
        {
            return BadRequest($"El autor \"{CreateAuthorDTO.Name}\" ya existe.");
        }

        var author = _mapper.Map<Author>(CreateAuthorDTO);

        _context.Add(author);
        await _context.SaveChangesAsync();

        var authorDTO = _mapper.Map<CreateAuthorDTO>(author);

        return CreatedAtRoute("ObtenerAutor", new { id = author.Id }, authorDTO);
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateAuthor(CreateAuthorDTO CreateAuthorDTO, int id)
    {
        var authorExists = await _context.Authors.AnyAsync(a => a.Id == id);

        if (!authorExists)
        {
            return NotFound($"El autor con el id {id} no fue encontrado");
        }

        var author = _mapper.Map<Author>(CreateAuthorDTO);

        author.Id = id;

        _context.Update(author);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author == null)
        {
            return NotFound();
        }

        _context.Remove(author);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}