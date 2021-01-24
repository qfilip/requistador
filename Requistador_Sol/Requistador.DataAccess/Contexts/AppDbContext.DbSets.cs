using Microsoft.EntityFrameworkCore;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.DataAccess.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Excerpt> Excerpts { get; set; }
    }
}
