using BookingSystem.Application;
using BookingSystem.Application.Interfaces;
using BookingSystem.Application.Services;
using BookingSystem.Infrastructure;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<BookingSystemContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                    });
            });

            var app = builder.Build();

            app.UseCors("ReactPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
