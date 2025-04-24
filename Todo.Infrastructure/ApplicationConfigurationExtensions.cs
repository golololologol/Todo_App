using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
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
                // Read either explicit CONNECTION_STRING or DATABASE_URL for Postgres
                var envConn = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                var dbUrl   = Environment.GetEnvironmentVariable("DATABASE_URL");
                var conn    = !string.IsNullOrWhiteSpace(envConn) ? envConn : dbUrl;

                if (!string.IsNullOrWhiteSpace(conn) && (
                        conn.TrimStart().StartsWith("Host=") ||
                        conn.TrimStart().StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
                        conn.TrimStart().StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)
                    ))
                {
                    // If using a URI, convert it to key=value format
                    var npgsqlConn = conn.TrimStart().StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
                                     conn.TrimStart().StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase)
                        ? new NpgsqlConnectionStringBuilder(conn).ConnectionString
                        : conn;
                    options.UseNpgsql(npgsqlConn,
                        opts => opts.MigrationsAssembly(
                            typeof(DatabaseContext).Assembly.GetName().Name));
                }
                else
                {
                    // Fallback to SQLite if no Postgres URL
                    var sqliteConn = !string.IsNullOrWhiteSpace(conn)
                                     ? conn
                                     : "Data Source=todo.db";
                    options.UseSqlite(sqliteConn,
                        opts => opts.MigrationsAssembly(
                            typeof(DatabaseContext).Assembly.GetName().Name));
                }
            });

            services.AddScoped<DatabaseContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITodoListRepository, TodoListRepository>();
            services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();

            return services;
        }
    }
}
