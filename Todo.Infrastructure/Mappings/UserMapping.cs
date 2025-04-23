using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.Models;

namespace Todo.Infrastructure.Mappings
{
    internal class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", "dbo");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Name).HasColumnName("name").IsRequired();
            builder.Property(e => e.Password).HasColumnName("password").IsRequired();
            builder.Property(e => e.IsAdmin).HasColumnName("is_admin").HasDefaultValue(false);

            builder.HasMany(e => e.Todos)
                .WithOne(e => e.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(e => e.Name).IsUnique();
        }
    }
}
