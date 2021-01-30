using AutoMapper;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BaseEntity, BaseDto>();
            CreateMap<BaseDto, BaseEntity>();

            CreateMap<Cocktail, CocktailDto>();
            CreateMap<CocktailDto, Cocktail>();

            CreateMap<Ingredient, IngredientDto>();
            CreateMap<IngredientDto, Ingredient>();

            CreateMap<Excerpt, ExcerptDto>();
            CreateMap<ExcerptDto, Excerpt>();
            //.ForMember(dest => dest.Id, m => m.MapFrom(src => src.Id));
        }
    }
}
