using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Repositories;

namespace Todo.Infrastructure
{
    public static class ApplicationConfigurationExtensions
    {
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                var conn = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                           ?? "Host=localhost;Database=todo.db";
                options.UseNpgsql(conn,
                    opts => opts.MigrationsAssembly(
                        typeof(DatabaseContext).Assembly.GetName().Name));
            });

            services.AddScoped<DatabaseContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoListRepository, TodoListRepository>();
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

            return services;
        }
    }
}
