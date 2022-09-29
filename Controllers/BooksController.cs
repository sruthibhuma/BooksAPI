using Microsoft.AspNetCore.Mvc;
using BooksAPI.Data.Models;
using BooksAPI.Data;

namespace BooksAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{

    private readonly AppDbContext _dbContext;

    public BooksController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

     [HttpGet("GetAllBooks")]
     public IActionResult GetAllBooks()
     {
         var allbooks = _dbContext.Books.ToList();
         return Ok(allbooks);
     }

    [HttpGet("GetBookById/{bookId}")]
    public IActionResult GetBookById(int bookId)
    {
        var book = _dbContext.Books.FirstOrDefault(book => book.Id == bookId);
        return Ok(book);
    }

        
    [HttpPost("AddBook")]
    public  IActionResult AddBook([FromBody] Book book)
    {
        var _book = new Book() { Name = book.Name, Author = book.Author };
        _dbContext.Books.Add(_book);
        _dbContext.SaveChanges();
        return Ok();
    }

    [HttpPut("UpdateBook/{bookId}")]
    public  IActionResult UpdateBook(int bookId, [FromBody] Book book)
    {
        var _book = _dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
        if(_book != null){
            _book.Name = book.Name;
            _book.Author = book.Author;

            _dbContext.SaveChanges();
        }
        
        return Ok(_book);
    }

    [HttpDelete("Delete/{bookId}")]
     public IActionResult Delete(int bookId)
     {
         var _book = _dbContext.Books.FirstOrDefault(b => b.Id == bookId);
        if(_book != null){
            
            _dbContext.Books.Remove(_book);
            _dbContext.SaveChanges();
        }

        return Ok();
     }
}
