using Microsoft.EntityFrameworkCore;
using Requistador.Domain.Base;
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

            SetDefaultConfiguration<Cocktail>(modelBuilder);
            SetDefaultConfiguration<Ingredient>(modelBuilder);
            SetDefaultConfiguration<Excerpt>(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        protected void SetDefaultConfiguration<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
        {
            builder.Entity<TEntity>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });
        }
    }
}
