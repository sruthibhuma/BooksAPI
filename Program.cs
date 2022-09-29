using System;
using Microsoft.Extensions.Configuration;

namespace BooksAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appSettings.json")
                                    .Build();

                // TODO : Implement loging

                CreateHostBuilder(args).Build().Run();
            }

            finally 
            {

            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => 
                        Host.CreateDefaultBuilder(args)
                        .ConfigureWebHostDefaults(webBuilder => 
                        {
                            webBuilder.UseStartup<Startup>();
                        });
                        
    }
}