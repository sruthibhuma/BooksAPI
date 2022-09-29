using System;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Data;
using BooksAPI.Data.Services;

namespace BooksAPI
{
    public class Startup
    {
        public string ConnectionString { get; set; }

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("connectionString");
        }

        //Add Services
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Configure DBContext
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(ConnectionString));
            
            //Configure Services 
            services.AddTransient<BooksService>();
            services.AddSwaggerGen();

        }


            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BooksAPI"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            //Exception Handling
           // app.ConfigureBuildInExceptionHandler(loggerFactory);
            //app.ConfigureCustomExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer.Seed(app);
            AppDbInitializer.Seed(app);
        }
    }
}