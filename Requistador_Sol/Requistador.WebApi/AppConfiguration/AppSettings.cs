using Mapster;
using Microsoft.IdentityModel.Tokens;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using Requistador.Identity.Dtos;
using Requistador.Identity.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public class AppSettings
    {
        public AppSettings(string appDbPath, string requestDbPath, string identityDbPath)
        {
            AppDbConnString = appDbPath;
            RequestDbConnString = requestDbPath;
            IdentityDbConnString = identityDbPath;
        }

        public string IdentityDbConnString { get; private set; }
        public string AppDbConnString { get; private set; }
        public string RequestDbConnString { get; private set; }
        
        public static SymmetricSecurityKey GetAppKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.Auth_SecretKey));

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
            conf.NewConfig<AppUser, AppUserDto>();
            conf.NewConfig<AppUserDto, AppUser>();
        }
    }
}
