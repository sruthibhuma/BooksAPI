using NLog.Web;

namespace BooksAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Inside Main method");
                var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appSettings.json")
                                    .Build();

                CreateHostBuilder(args).Build().Run();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error in Main Method");
            }

            finally 
            {
                NLog.LogManager.Shutdown();
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