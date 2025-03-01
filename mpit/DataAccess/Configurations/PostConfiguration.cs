using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id); // Указание первичного ключа, если Id – это свойство базового класса BaseEntity

        builder.Property(p => p.Name).HasColumnName("Post_Name").IsRequired();

        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();

        builder.Property(p => p.AuthorId).HasColumnName("Author_Id").IsRequired();
    }
}
