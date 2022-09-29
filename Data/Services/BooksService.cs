using System;
using BooksAPI.Data;
using BooksAPI.Data.Models;
using BooksAPI.Data.Dtos;
using Microsoft.Extensions.Logging;

namespace BooksAPI.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;
        private readonly ILogger<BooksService> _logger;

        public BooksService(AppDbContext context, ILogger<BooksService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) 
        {
            _logger.Log(LogLevel.Debug, "Getting a book by Id");
            var _book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            return _book;
        }

        public int AddBook(BookDto book)
        {
            var _book = new Book() { Name = book.Name, Author = book.Author };
            _context.Books.Add(_book);
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "Book is Created");
            return _book.Id;
        }

        public Book UpdateBook(int bookId, BookDto book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null)
            {
                _book.Name = book.Name;
                _book.Author = book.Author;
                _context.SaveChanges();

                return _book;
            }
            return null;
        }

        public bool Deletebook(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            if(_book != null)
            {
                 _context.Books.Remove(_book);
                 _context.SaveChanges();
                 return true;
            }

            return false;
        }
    }
}