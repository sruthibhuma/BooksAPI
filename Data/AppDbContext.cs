using System;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Data.Models;

namespace BooksAPI.Data{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

        
    }
}