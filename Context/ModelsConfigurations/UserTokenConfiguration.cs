using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.DbModels;

namespace Context.ModelsConfigurations
{
    public class UserTokenConfiguration : IGlobalConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RefreshTokenIdHash).HasMaxLength(450);
            builder.Property(x => x.RefreshTokenIdHashSource).HasMaxLength(450);
        }
    }
}
