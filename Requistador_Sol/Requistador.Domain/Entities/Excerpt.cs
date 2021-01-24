using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Entities
{
    public class Excerpt : BaseEntity
    {
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }
        public int Amount { get; set; }

        public virtual Cocktail Cocktail { get; set; }
        public virtual Ingredient Ingredient { get; set; }
    }
}
