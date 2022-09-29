using Microsoft.AspNetCore.Mvc;


namespace BooksAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    
    // private readonly BooksContext _DBContext;

    // public BooksController(BooksContext _dbContext)
    // {
    //     this._DBContext = _dbContext;
    // }

    // [HttpGet(Name = "GetAll")]
    // public IActionResult GetAll()
    // {
    //     var books = this._DBContext.Books.ToList();
    //     return Ok(books);
    // }

}
