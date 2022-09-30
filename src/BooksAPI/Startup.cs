using System;
using Microsoft.EntityFrameworkCore;
using BooksAPI.Data;
using BooksAPI.Data.Services;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Data.SqlClient;
using AutoMapper;
using BooksAPI.Handlers;


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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });                
            });
            
            services.AddLogging(loggingBuilder => {loggingBuilder.AddNLog("nlog.config");
            });

            var autoMapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
            IMapper mapper = autoMapper.CreateMapper();
            services.AddSingleton(mapper);


            //adding health check services to container
            services.AddHealthChecks()
             //.AddDbContextCheck<AppDbContext>()
             .AddCheck("sql", () => {  //TODO: Move to a separate class
                using(var connection = new SqlConnection(ConnectionString)) {  
                try {  
                    connection.Open();  
                } catch (SqlException) {  
                    return HealthCheckResult.Unhealthy();  
                }  
            }  
                return HealthCheckResult.Healthy();  
            });


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

            app.UseHealthChecks("/Health");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer.Seed(app);
            AppDbInitializer.Seed(app);
        }
    }
}