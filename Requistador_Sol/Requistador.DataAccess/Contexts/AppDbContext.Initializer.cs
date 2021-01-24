using Microsoft.EntityFrameworkCore;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.DataAccess.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>().ToTable(nameof(Cocktail));
            modelBuilder.Entity<Ingredient>().ToTable(nameof(Ingredient));
            modelBuilder.Entity<Excerpt>().ToTable(nameof(Excerpt));

            modelBuilder.Entity<Cocktail>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            modelBuilder.Entity<Ingredient>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            modelBuilder.Entity<Excerpt>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
