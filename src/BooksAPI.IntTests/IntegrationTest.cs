using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using BooksAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;


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
    }
}