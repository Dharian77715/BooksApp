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
    public async Task<ActionResult> CreateComment(int bookId, CreateCommentDTO CreateCommentDTO)
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
    public async Task<ActionResult> UpdateComment(int bookId, int id, CreateCommentDTO CreateCommentDTO)
    {
        var bookExists = await _context.Books.AnyAsync(bookDB => bookDB.Id == bookId);

        if (!bookExists)
        {
            return NotFound();
        }

        var commentExists = await _context.Comments.AnyAsync(commentDB => commentDB.Id == id);

        if (!commentExists)
        {
            return NotFound();
        }

        var comment = _mapper.Map<Comment>(CreateCommentDTO);
        comment.Id = id;
        comment.BookId = bookId;
        _context.Update(comment);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        _context.Remove(comment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}