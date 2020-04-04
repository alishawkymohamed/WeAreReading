using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbModels;

namespace Context.ModelsConfigurations
{
    public class GovermentConfiguration : IGlobalConfiguration<Government>
    {
        public void Configure(EntityTypeBuilder<Government> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValue(DateTime.Now);
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
