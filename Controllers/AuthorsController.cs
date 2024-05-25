using AutoMapper;
using BooksApp;
using BooksApp.DTOs;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BooksApp.Services;


namespace Authors.Controllers;

[ApiController]
[Route("api/autores")]
public class AuthorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IFileManager _fileManager;
    private readonly string container = "autores";

    public AuthorsController(ApplicationDbContext context, IMapper mapper, IFileManager fileManager)
    {
        _context = context;
        _mapper = mapper;
        _fileManager = fileManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuthorsDTO>>> GetAuthors()
    {
        var authors = await _context.Authors
        .Include(a => a.Sex)
        .ToListAsync();
        return _mapper.Map<List<AuthorsDTO>>(authors);
    }

    [HttpGet("{id:int}", Name = "ObtenerAutor")]
    public async Task<ActionResult<List<AuthorsDTO>>> GetAuthorById(int id)
    {

        var author = await _context.Authors
        .Include(a => a.Sex)
        .SingleOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound($"El autor con el id {id} no fue encontrado");
        }

        var AuthorsDTO = _mapper.Map<AuthorsDTO>(author);
        return Ok(AuthorsDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CreateAuthor([FromForm] CreateAuthorDTO CreateAuthorDTO)
    {

        var authorExists = await _context.Authors.AnyAsync(a => a.Name == CreateAuthorDTO.Name);

        if (authorExists)
        {
            return BadRequest($"El autor \"{CreateAuthorDTO.Name}\" ya existe.");
        }

        var author = _mapper.Map<Author>(CreateAuthorDTO);

        if (CreateAuthorDTO.Photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await CreateAuthorDTO.Photo.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(CreateAuthorDTO.Photo.FileName);
                author.Photo = await _fileManager.SaveFile(content, extension, container,
                    CreateAuthorDTO.Photo.ContentType);
            }
        }

        _context.Add(author);
        await _context.SaveChangesAsync();

        var authorDTO = _mapper.Map<CreateAuthorDTO, Author>(CreateAuthorDTO);

        return CreatedAtRoute("ObtenerAutor", new { id = author.Id }, authorDTO);
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateAuthor([FromForm] CreateAuthorDTO CreateAuthorDTO, int id)
    {

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }

        author = _mapper.Map(CreateAuthorDTO, author);


        if (CreateAuthorDTO.Photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await CreateAuthorDTO.Photo.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(CreateAuthorDTO.Photo.FileName);
                author.Photo = await _fileManager.EditFile(content, extension, container,
                author.Photo, CreateAuthorDTO.Photo.ContentType);
            }
        }


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