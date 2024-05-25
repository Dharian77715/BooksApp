using AutoMapper;
using BooksApp;
using BooksApp.DTOs;
using BooksApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPIAutores.Controllers;

[ApiController]
[Route("api/libros/{bookId:int}/comentarios")]
public class ComentariosController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ComentariosController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentDTO>>> GetComments(int bookId)
    {
        var bookExists = await _context.Books.AnyAsync(bookDB => bookDB.Id == bookId);

        if (!bookExists)
        {
            return NotFound();
        }

        var comments = await _context.Comments
            .Where(commentDB => commentDB.BookId == bookId).ToListAsync();

        return _mapper.Map<List<CommentDTO>>(comments);
    }

    [HttpGet("{id:int}", Name = "ObtenerComentario")]
    public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(commentDB => commentDB.Id == id);

        if (comment == null)
        {
            return NotFound();
        }

        return _mapper.Map<CommentDTO>(comment);
    }

    [HttpPost]
    public async Task<ActionResult> Post(int bookId, CreateCommentDTO CreateCommentDTO)
    {
        var bookExists = await _context.Books.AnyAsync(bookDB => bookDB.Id == bookId);

        if (!bookExists)
        {
            return NotFound();
        }

        var comment = _mapper.Map<Comment>(CreateCommentDTO);
        comment.BookId = bookId;
        _context.Add(comment);
        await _context.SaveChangesAsync();

        var commentDTO = _mapper.Map<CommentDTO>(comment);

        return CreatedAtRoute("ObtenerComentario", new { id = comment.Id, bookId = bookId }, commentDTO);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int libroId, int id, CreateCommentDTO CreateCommentDTO)
    {
        var existeLibro = await _context.Books.AnyAsync(libroDB => libroDB.Id == libroId);

        if (!existeLibro)
        {
            return NotFound();
        }

        var existeComentario = await _context.Comments.AnyAsync(comentarioDB => comentarioDB.Id == id);

        if (!existeComentario)
        {
            return NotFound();
        }

        var comentario = _mapper.Map<Comment>(CreateCommentDTO);
        comentario.Id = id;
        comentario.BookId = libroId;
        _context.Update(comentario);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}