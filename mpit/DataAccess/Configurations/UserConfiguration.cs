using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mpit.DataAccess.Entities;

namespace mpit.DataAccess.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id); // Указание первичного ключа, если Id – это свойство базового класса BaseEntity

        builder.Property(u => u.FirstName).HasColumnName("First_Name").IsRequired();

        builder.Property(u => u.PasswordHash).HasColumnName("Password_Hash").IsRequired();

        builder.Property(u => u.Role).HasColumnName("Role").IsRequired();

        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();

        builder.Property(u => u.Coins);

        builder.HasMany(u => u.Posts).WithOne(p => p.Author).HasForeignKey(p => p.AuthorId);
    }
}
