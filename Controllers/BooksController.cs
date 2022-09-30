using Microsoft.AspNetCore.Mvc;
using BooksAPI.Data.Models;
using BooksAPI.Data;
using BooksAPI.Data.Services;
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
         
         var allBooks = _booksService.GetAllBooks();
         if(allBooks.Count() == 0)
            return NotFound();
         return Ok(allBooks);
     }

    [HttpGet("GetBookById/{bookId}")]
    public IActionResult GetBookById(int bookId)
    {
        var book = _booksService.GetBookById(bookId);
        if(book == null)
            return NotFound();

        return Ok(book);
    }
        
    [HttpPost("AddBook")]
    public  IActionResult AddBook([FromBody] BookDto book)
    {
        if(!ModelState.IsValid)
             return BadRequest("Invalid Data");
        int createdBookId = _booksService.AddBook(book);
             
        return Created($"api/AddBook/{createdBookId}", book);
    }

    [HttpPut("UpdateBook/{bookId}")]
    public  IActionResult UpdateBook(int bookId, [FromBody] BookDto book)
    {
        try {
        if(!ModelState.IsValid)
             return BadRequest("Invalid Data");
             
        var updatedBook = _booksService.UpdateBook(bookId, book);
        if(updatedBook == null)
            return NotFound();

        return Ok(updatedBook);
        }
        catch(Exception ex)
        {
            return  StatusCode(StatusCodes.Status500InternalServerError, ex);         }
        }

    [HttpDelete("Delete/{bookId}")]
     public IActionResult Delete(int bookId)
     {
        bool isSuccess = _booksService.Deletebook(bookId);

        if(isSuccess)
            return NoContent();
        else
            return NotFound();
     }
}
