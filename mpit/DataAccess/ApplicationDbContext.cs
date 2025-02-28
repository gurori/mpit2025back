using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : DbContext(options) {

    public DbSet<UserEntity> Users {get; set;}
    public DbSet<Test> Tests {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

public partial class TestConfiguration : IEntityTypeConfiguration<Test> {

    public void Configure(EntityTypeBuilder<Test> builder) {

        builder.HasKey(x => x.Id);

        // builder.Property(x => x.Age);

        builder.HasData(
            new Test(Guid.NewGuid(), "MTF", 18),
            new Test(Guid.NewGuid(), "wha", 11),
            new Test(Guid.NewGuid(), "cool", 4),
            new Test(Guid.NewGuid(), "", 345),
            new Test(Guid.NewGuid(), "MTF", 2)
            );
    }
}

public record Test(
    Guid Id,
    string Title,
    int Age
);