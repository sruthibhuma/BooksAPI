using System;
using BooksAPI.Data;
using BooksAPI.Data.Models;
using BooksAPI.Data.Dtos;

namespace BooksAPI.Data.Services
{
    public class BooksService
    {
        private AppDbContext _context;

        public BooksService(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public Book GetBookById(int bookId) 
        {
            var _book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            return _book;
        }

        public void AddBook(BookDto book)
        {
            var _book = new Book() { Name = book.Name, Author = book.Author };
            _context.Books.Add(_book);
            _context.SaveChanges();
        }

        public Book UpdateBook(int bookId, BookDto book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if(_book != null)
            {
                _book.Name = book.Name;
                _book.Author = book.Author;
                _context.SaveChanges();
            }
            return _book;
        }

        public void Deletebook(int bookId)
        {
            var _book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            if(_book != null)
            {
                 _context.Books.Remove(_book);
                 _context.SaveChanges();
            }
        }
    }
}