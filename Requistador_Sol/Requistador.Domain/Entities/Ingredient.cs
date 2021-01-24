using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Entities
{
    public class Ingredient : BaseEntity
    {
        public Ingredient()
        {
            Excerpts = new HashSet<Excerpt>();
        }
        public string Name { get; set; }
        public int Strength { get; set; }
        public virtual ICollection<Excerpt> Excerpts { get; set; }
    }
}
