using System;
using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Data.Dtos{

    public class BookDto {
        
        [Required]
        public string Name { get; set;}
        public string Author { get; set; }

    }
}