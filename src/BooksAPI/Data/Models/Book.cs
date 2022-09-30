using System;
using System.ComponentModel.DataAnnotations;

namespace BooksAPI.Data.Models{

    public class Book {

        public int Id { get; set; }
        [Required]
        public string Name { get; set;}
        public string Author { get; set; }

    }
}