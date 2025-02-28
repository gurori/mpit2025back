using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using mpit.DataAccess.Entities;

namespace mpit.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity> {

    public void Configure(EntityTypeBuilder<UserEntity> builder) {

        builder.HasKey(x => x.Id);


    }
}