using System.Linq;
using Context.DatabaseExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.DbModels;
using Models.DbModels.TrackingInterfaces;

namespace Context
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {

        }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (IMutableEntityType type in modelBuilder.Model.GetEntityTypes().Where(type => typeof(IAuditableDelete).IsAssignableFrom(type.ClrType)))
            {
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }

            modelBuilder.ApplyModelsConfigurations();

            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=.;Database=WeAreReading;Trusted_Connection=True;ConnectRetryCount=0");
        }

        #region Database Models
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Book> Books { get; set; }
        #endregion
    }
}
