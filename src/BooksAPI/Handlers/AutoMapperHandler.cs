using AutoMapper;
using BooksAPI.Data.Models;
using BooksAPI.Data.Dtos;


namespace BooksAPI.Handlers
{
    public class AutoMapperHandler : Profile
    {
        public AutoMapperHandler()
        {
            CreateMap<Book, BookDto>().ReverseMap();
        }
    }

}