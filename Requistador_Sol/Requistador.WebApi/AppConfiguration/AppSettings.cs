using Mapster;
using Microsoft.IdentityModel.Tokens;
using Requistador.Domain.Base;
using Requistador.Domain.Dtos;
using Requistador.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Requistador.WebApi.AppConfiguration
{
    public class AppSettings
    {
        public AppSettings(string appDbPath, string liteDbPath)
        {
            DbConnectionString = appDbPath;
            LiteDbPath = liteDbPath;
        }

        public string DbConnectionString { get; private set; }
        public string LiteDbPath { get; private set; }
        
        public static SymmetricSecurityKey GetAppKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConstants.Auth_SecretKey));

        public static TypeAdapterConfig GetMapsterConfiguration()
        {
            var conf = new TypeAdapterConfig();
            
            conf.NewConfig<BaseEntity, BaseDto>();
            conf.NewConfig<BaseDto, BaseEntity>();

            conf.NewConfig<Cocktail, CocktailDto>();
            conf.NewConfig<CocktailDto, Cocktail>();

            conf.NewConfig<Ingredient, IngredientDto>();
            conf.NewConfig<IngredientDto, Ingredient>();

            conf.NewConfig<Excerpt, ExcerptDto>();
            conf.NewConfig<ExcerptDto, Excerpt>();

            
            return conf;
        }
    }
}
