#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
using Todo.Domain.Models;
using Todo.Infrastructure.Mappings;

namespace Todo.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TodoList> Todos { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }

        public DatabaseContext(DbContextOptions options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new TodoListMapping());
            modelBuilder.ApplyConfiguration(new TodoTaskMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(
                    "Data Source = main_db",
                    options => options.MigrationsAssembly(
                        typeof(DatabaseContext).Assembly.GetName().Name));
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
#pragma warning restore CS8618