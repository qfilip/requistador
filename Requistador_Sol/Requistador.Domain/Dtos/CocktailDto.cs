using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Dtos
{
    public class CocktailDto : BaseDto
    {
        public string Name { get; set; }
        public List<ExcerptDto> Excerpts { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
    }
}
