using Context.ModelsConfigurations;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;

namespace Context.DatabaseExtensions
{
    public static class ApplyConfigurationExtenstion
    {
        public static void ApplyModelsConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Role>(new RoleConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<UserRole>(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration<UserToken>(new UserTokenConfiguration());
        }
    }
}
