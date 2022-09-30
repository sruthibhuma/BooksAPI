using System;
using BooksAPI.Data;
using BooksAPI.Data.Models;
using BooksAPI.Data.Dtos;
using AutoMapper;

namespace BooksAPI.Data.Services
{
    public class BooksService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BooksService> _logger;
        private readonly IMapper _mapper;

        public BooksService(AppDbContext context, ILogger<BooksService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public List<Book> GetAllBooks() => _context.Books.ToList();
        public BookDto GetBookById(int bookId) 
        {
            _logger.Log(LogLevel.Debug, "Getting a book by Id");
            var _book = _context.Books.FirstOrDefault(book => book.Id == bookId);
            var _bookDto = _mapper.Map<Book,BookDto>(_book);
            
            return _bookDto;
        }

        public int AddBook(BookDto book)
        {
            var _book = _mapper.Map<BookDto,Book>(book); 
           
            try {
            _context.Books.Add(_book);
            _context.SaveChanges();
            _logger.Log(LogLevel.Information, "Book is Created");    
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error in adding a book");
            }
            return _book.Id;
        }

        public Book UpdateBook(int bookId, BookDto book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            
            try 
            {
            if(_book != null)
            {
                _book.Name = book.Name;
                _book.Author = book.Author;
                _context.SaveChanges();
                _logger.Log(LogLevel.Information, "Book is Updated"); 
                return _book;
            }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex, "Error in updating the book"); 
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