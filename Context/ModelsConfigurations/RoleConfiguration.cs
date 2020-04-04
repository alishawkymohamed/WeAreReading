using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbModels;

namespace Context.ModelsConfigurations
{
    public class RoleConfiguration : IGlobalConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedAt).HasDefaultValue(null);

            builder.HasData(
                new Role { Id = 1, Name = "User", CreatedAt = DateTime.Now, DeletedAt = null, IsDeleted = false },
                new Role { Id = 2, Name = "Library", CreatedAt = DateTime.Now, DeletedAt = null, IsDeleted = false }
                );
        }
    }
}
