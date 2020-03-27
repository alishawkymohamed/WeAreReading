using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbModels;

namespace Context.ModelsConfigurations
{
    public class UserRoleConfiguration : IGlobalConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
