using Microsoft.EntityFrameworkCore;
using TP24Entities;
using TP24LendingApi.CustomExceptionMiddleware;

namespace TP24LendingApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ReceivablesContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDatabase")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
                using (var context = scope.ServiceProvider.GetService<ReceivablesContext>())
                    context?.Database.EnsureCreated();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseMiddleware<ExceptionMiddleware>();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}