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
}
