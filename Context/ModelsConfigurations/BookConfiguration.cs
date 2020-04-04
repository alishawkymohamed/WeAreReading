using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbModels;

namespace Context.ModelsConfigurations
{
    public class BookConfiguration : IGlobalConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(450);
            builder.Property(x => x.Author).HasMaxLength(450);
            builder.Property(x => x.Description).HasMaxLength(900);
            builder.Property(x => x.Title).IsRequired();
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.DeletedAt).HasDefaultValue(null);
        }
    }
}
