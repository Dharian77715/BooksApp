using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BooksApp.Controllers;

[ApiController]
[Route("api/libros")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BooksController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<BooksDTO>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<List<BooksDTO>>(books);
    }

    [HttpGet("{id:int}", Name = "ObtenerLibro")]
    public async Task<ActionResult<List<BooksDTO>>> GetBookById(int id)
    {

        var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound($"El libro con el id {id} no fue encontrado");
        }

        var BooksDTO = _mapper.Map<BooksDTO>(book);
        return Ok(BooksDTO);
    }

    [HttpPost]
    public async Task<ActionResult> CreateBook([FromBody] CreateBookDTO CreateBookDTO)
    {

        var bookExists = await _context.Books.AnyAsync(b => b.Title == CreateBookDTO.Title);

        if (bookExists)
        {
            return BadRequest($"El libro \"{CreateBookDTO.Title}\" ya existe.");
        }

        var book = _mapper.Map<Book>(CreateBookDTO);

        _context.Add(book);
        await _context.SaveChangesAsync();

        var bookDTO = _mapper.Map<CreateBookDTO>(book);

        return CreatedAtRoute("ObtenerLibro", new { id = book.Id }, bookDTO);
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateBook(CreateBookDTO CreateBookDTO, int id)
    {
        var bookExists = await _context.Books.AnyAsync(b => b.Id == id);

        if (!bookExists)
        {
            return NotFound($"El libro con el id {id} no fue encontrado");
        }

        var book = _mapper.Map<Book>(CreateBookDTO);

        book.Id = id;

        _context.Update(book);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        _context.Remove(book);
        await _context.SaveChangesAsync();
        return NoContent();
    }

}