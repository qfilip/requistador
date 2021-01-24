using Requistador.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Requistador.Domain.Dtos
{
    public class IngredientDto : BaseDto
    {
        public string Name { get; set; }
        public int Strength { get; set; }
        public List<ExcerptDto> Excerpts { get; set; }
    }
}
