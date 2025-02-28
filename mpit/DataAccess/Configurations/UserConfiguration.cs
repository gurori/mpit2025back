using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity> {

    public void Configure(EntityTypeBuilder<UserEntity> builder) {

        builder.HasKey(x => x.Id);


    }
}