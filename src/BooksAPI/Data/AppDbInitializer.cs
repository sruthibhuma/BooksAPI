using BooksAPI.Data.Models;

namespace BooksAPI.Data {

public class AppDbInitializer
{
    public static void Seed(IApplicationBuilder applicationBuilder)
    {
        using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

            if(!context.Books.Any())
            {
                context.Books.AddRange( new Book()
                 {
                    Name = "Rich Dad Poor Dad",
                    Author = "Robert Kiyosaki"
                 },
                 
                 new Book()
                 {
                     Name = "Pride & Prejudice",
                     Author = "Jane Austen"
                 },
                 new Book()
                 {
                     Name = "Three Thousand Stiches",
                     Author = "Sudha Murty"
                 },
                new Book()
                 {
                     Name = "Wise and Otherwise",
                     Author = "Sudha Murty"
                 },
                 new Book()
                 {
                     Name = "Murder on the orient Express",
                     Author = "Agata Christie"
                 });

                context.SaveChanges();
            }
        }
    }
}

}