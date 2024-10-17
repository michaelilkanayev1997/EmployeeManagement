using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Repositories;

namespace server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(
                options => options.UseInMemoryDatabase("EmployeeDb"));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCors", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            // add the employee repository to the DI (dependency injection)
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseCors("MyCors");

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
