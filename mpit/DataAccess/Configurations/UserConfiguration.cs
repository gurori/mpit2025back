using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mpit.DataAccess.Entities;

namespace mpit.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        // Настройка свойств
        builder
            .Property(u => u.FirstName)
            .IsRequired() // Обязательное поле
            .HasMaxLength(100); // Максимальная длина

        builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(256);

        builder.Property(u => u.Role).IsRequired().HasMaxLength(50);

        builder.Property(u => u.Login).IsRequired().HasMaxLength(256);
    }
}
