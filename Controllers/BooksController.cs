using Microsoft.AspNetCore.Mvc;
using BooksAPI.Data.Models;
using BooksAPI.Data;
using BooksAPI.Services;
using BooksAPI.Data.Dtos;

namespace BooksAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{

    private BooksService _booksService;
        public BooksController(BooksService booksService)
        {
            _booksService = booksService;
        }

   
     [HttpGet("GetAllBooks")]
     public IActionResult GetAllBooks()
     {
         var allbooks = _booksService.GetAllBooks();
         return Ok(allbooks);
     }

    [HttpGet("GetBookById/{bookId}")]
    public IActionResult GetBookById(int bookId)
    {
        var book = _booksService.GetBookById(bookId);
        return Ok(book);
    }
        
    [HttpPost("AddBook")]
    public  IActionResult AddBook([FromBody] BookDto book)
    {
        _booksService.AddBook(book);
        return Ok();
    }

    [HttpPut("UpdateBook/{bookId}")]
    public  IActionResult UpdateBook(int bookId, [FromBody] BookDto book)
    {
        var updatedBook = _booksService.UpdateBook(bookId, book);
        return Ok(updatedBook);
    }

    [HttpDelete("Delete/{bookId}")]
     public IActionResult Delete(int bookId)
     {
        _booksService.Deletebook(bookId);

        return Ok();
     }
}
