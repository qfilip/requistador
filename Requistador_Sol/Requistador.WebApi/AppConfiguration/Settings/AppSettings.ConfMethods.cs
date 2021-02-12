using Mapster;
using Requistador.Domain.Base;
using Requistador.Domain.Entities;
using Requistador.Dtos.Domain;

namespace Requistador.WebApi.AppConfiguration.Settings
{
    public sealed partial class AppSettings
    {
        public static TypeAdapterConfig GetMapsterConfiguration()
        {
            var conf = new TypeAdapterConfig();

            DefineDomainMapping(conf);
            DefineIdentityMapping(conf);

            return conf;
        }

        private static void DefineDomainMapping(TypeAdapterConfig conf)
        {
            conf.NewConfig<BaseEntity, BaseDto>();
            conf.NewConfig<BaseDto, BaseEntity>();

            conf.NewConfig<Cocktail, CocktailDto>();
            conf.NewConfig<CocktailDto, Cocktail>();

            conf.NewConfig<Ingredient, IngredientDto>();
            conf.NewConfig<IngredientDto, Ingredient>();

            conf.NewConfig<Excerpt, ExcerptDto>();
            conf.NewConfig<ExcerptDto, Excerpt>();
        }

        private static void DefineIdentityMapping(TypeAdapterConfig conf)
        {
            //conf.NewConfig<AppUser, AppUserDto>();
            //conf.NewConfig<AppUserDto, AppUser>();
        }
    }
}
