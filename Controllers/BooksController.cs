﻿using AutoMapper;
using BooksApp.DTOs;
using BooksApp.Models;
using BooksApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BooksApp.Controllers;

[ApiController]
[Route("api/libros")]
public class BooksController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    private readonly IFileManager _fileManager;
    private readonly string container = "libros";

    public BooksController(ApplicationDbContext context, IMapper mapper, IFileManager fileManager)
    {
        _context = context;
        _mapper = mapper;
        _fileManager = fileManager;
    }

    [HttpGet]
    public async Task<ActionResult<List<BooksDTO>>> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<List<BooksDTO>>(books);
    }


    [HttpGet("{id:int}", Name = "ObtenerLibro")]
    public async Task<ActionResult<BooksDTO>> GetBookById(int id)
    {
        var book = await _context.Books
            .Include(b => b.BooksGenres)
                .ThenInclude(bg => bg.Genre)
            .Include(bookDB => bookDB.Comments)
            .SingleOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound($"El libro con el id {id} no fue encontrado");
        }

        var booksDTO = _mapper.Map<BooksDTO>(book);
        return Ok(booksDTO);
    }


    [HttpPost]
    public async Task<ActionResult> CreateBook([FromForm] CreateBookDTO CreateBookDTO)
    {

        var bookExists = await _context.Books.AnyAsync(b => b.Title == CreateBookDTO.Title);

        if (bookExists)
        {
            return BadRequest($"El libro \"{CreateBookDTO.Title}\" ya existe.");
        }

        var book = _mapper.Map<Book>(CreateBookDTO);


        if (CreateBookDTO.Photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await CreateBookDTO.Photo.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(CreateBookDTO.Photo.FileName);
                book.Photo = await _fileManager.SaveFile(content, extension, container,
                    CreateBookDTO.Photo.ContentType);
            }
        }

        _context.Add(book);
        await _context.SaveChangesAsync();

        var bookDTO = _mapper.Map<CreateBookDTO, Book>(CreateBookDTO);

        return CreatedAtRoute("ObtenerLibro", new { id = book.Id }, bookDTO);
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> UpdateBook([FromForm] CreateBookDTO CreateBookDTO, int id)
    {
        var book = await _context.Books
        .Include(b => b.BooksGenres)
        .Include(b => b.Comments)
        .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        book = _mapper.Map(CreateBookDTO, book);


        if (CreateBookDTO.Photo != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await CreateBookDTO.Photo.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(CreateBookDTO.Photo.FileName);
                book.Photo = await _fileManager.EditFile(content, extension, container,
                book.Photo, CreateBookDTO.Photo.ContentType);
            }
        }

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