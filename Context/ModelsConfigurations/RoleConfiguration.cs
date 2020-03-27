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
            builder.Property(x => x.CreateAt).HasDefaultValue(DateTime.Now);
            builder.HasIndex(x => x.ArabicName).IsUnique();
            builder.HasIndex(x => x.EnglishName).IsUnique();
            builder.Property(x => x.EnglishName).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedAt).HasDefaultValue(null);

            builder.HasData(
                new Role { Id = 1, ArabicName = "مستخدم", EnglishName = "User", CreateAt = DateTime.Now, DeletedAt = null, IsDeleted = false },
                new Role { Id = 2, ArabicName = "صاحب مكتبة", EnglishName = "Library Owner", CreateAt = DateTime.Now, DeletedAt = null, IsDeleted = false }
                );
        }
    }
}
