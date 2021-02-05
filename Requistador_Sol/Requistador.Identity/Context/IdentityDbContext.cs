using Microsoft.EntityFrameworkCore;
using Requistador.Identity.Entites;
using Requistador.Identity.Enumerations;

namespace Requistador.Identity.Context
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppUser>().ToTable(nameof(AppUser));
            modelBuilder.Entity<AppUser>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.Status == eUserStatus.Active);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
