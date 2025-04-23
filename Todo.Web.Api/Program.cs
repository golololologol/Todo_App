using Todo.Infrastructure;
using Todo.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Api
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
            builder.Services.ConfigureDatabase();
            builder.Services.AddServices();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                // Command to use in the powershell
                // Goto the filder wher this file is located and open powershell
                // execute this:
                //      dotnet ef migrations add InitialMigration
                //          --project ..\Todo.Infrastructure\Todo.Infrastructure.csproj
                //          -o ..\Todo.Infrastructure\Migrations
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                context.Database.Migrate();
            }

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                await next(context);

                if (context.Response.StatusCode == 200)
                {
                    context.RequestServices
                        .GetRequiredService<DatabaseContext>()
                        .SaveChanges();
                }
            });

            app.Use(async (HttpContext context, RequestDelegate next) =>
            {
                try
                { 
                    await next(context);
                }
                catch(Exception ex)
                {
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(ex.Message);
                }
            });

            app.Run();
        }
    }
}
