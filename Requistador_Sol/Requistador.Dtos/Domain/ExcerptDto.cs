using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Dtos.Domain
{
    public class ExcerptDto : BaseDto
    {
        public string CocktailId { get; set; }
        public string IngredientId { get; set; }
        public int Amount { get; set; }
    }
}
