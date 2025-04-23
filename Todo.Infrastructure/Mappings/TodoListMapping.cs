using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Models;

namespace Todo.Infrastructure.Mappings
{
    internal class TodoListMapping : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.ToTable("todo_lists", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Date).HasColumnName("date").IsRequired();
            builder.Property(e => e.Description).HasColumnName("description").IsRequired();
            builder.Property(e => e.NumberOfTasks).HasColumnName("number_of_tasks");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasMany(e => e.Tasks)
                .WithOne(e => e.Holder)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Owner)
                .WithMany(e => e.Todos)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
