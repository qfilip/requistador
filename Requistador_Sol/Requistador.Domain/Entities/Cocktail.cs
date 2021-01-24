using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Entities
{
    public class Cocktail : BaseEntity
    {
        public Cocktail()
        {
            Excerpts = new HashSet<Excerpt>();
        }
        public string Name { get; set; }
        public virtual ICollection<Excerpt> Excerpts { get; set; }
    }
}
