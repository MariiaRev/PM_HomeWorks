using Microsoft.EntityFrameworkCore;

namespace DepsWebApp.Models
{
    /// <summary>
    /// Database context for the application  DepsWebApp
    /// </summary>
    public class DepsWebAppContext: DbContext
    {
        /// <summary>
        /// DbSet users represented by the model <see cref="User"/>.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Constructor with <paramref name="options"/>.
        /// </summary>
        /// <param name="options"></param>
        public DepsWebAppContext(DbContextOptions<DepsWebAppContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        /// <summary>
        /// Actions on model creating
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}
