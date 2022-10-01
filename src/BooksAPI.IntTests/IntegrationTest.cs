using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using BooksAPI.Data;
using BooksAPI.Data.Models;
using BooksAPI.Data.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text;
using Newtonsoft.Json;


namespace BooksAPI.IntTests
{
    public class IntegrationTest 
    {
        
        protected readonly HttpClient TestClient;
        
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            services.RemoveAll(typeof(AppDbContext));
                            services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                        });
                    });
            
            TestClient = appFactory.CreateClient();
        }

        [Fact]
        public async Task Test_Get_All_Books()
        {
           
            var response = await TestClient.GetAsync("/api/Books/GetAllBooks");

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
         
         }

        [Fact]
        public async Task Test_Get_Book_By_Id_Success()
        {
           
            var response = await TestClient.GetAsync("/api/Books/GetBookById/1");
            var responseName = response.Content.ReadAsAsync<Book>().Result;

            response.EnsureSuccessStatusCode();
            Assert.Equal("Rich Dad Poor Dad",  responseName.Name);
            
         }

        [Fact]
        public async Task Test_Get_Book_By_Id_Not_Found()
        {      
            var response = await TestClient.GetAsync("/api/Books/GetBookById/99");         
            Assert.Equal("NotFound",  response.StatusCode.ToString());         
        }

        [Fact]
        public async Task Test_Create_Book_Success()
        {      
            var addBook = new BookDto();
            addBook.Name = "John Doe";
            addBook.Author = "gardener";        

            var json = JsonConvert.SerializeObject(addBook);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await TestClient.PostAsync("/api/Books/AddBook", data);         
           
            string result = response.Content.ReadAsStringAsync().Result;
            
            Assert.Equal("Created", response.StatusCode.ToString());      
        }
    }
}