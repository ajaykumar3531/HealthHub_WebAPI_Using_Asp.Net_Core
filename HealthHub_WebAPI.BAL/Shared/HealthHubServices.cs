using HealthHub_WebAPI.BAL.Authentication_Repos.Contracts;
using HealthHub_WebAPI.BAL.Authentication_Repos.Services;
using HealthHub_WebAPI.BAL.Shared.JWT_Token;
using HealthHub_WebAPI.DAL.Authentication;
using HealthHub_WebAPI.DAL.Generic_Repos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.Shared
{
    public static class HealthHubServices
    {
        public static IServiceCollection AddHealthHubServices(this IServiceCollection services)
        {
            #region Db_Context
            services.AddDbContext<HealthHubAuthenticationContext>();
            #endregion

            #region GenericRepos
            services.AddScoped<IRepository<HealthHubAuthenticationContext, User>, Repository<HealthHubAuthenticationContext, User>>();
            services.AddScoped<IRepository<HealthHubAuthenticationContext,Role>, Repository<HealthHubAuthenticationContext, Role>>();
            services.AddScoped<IRepository<HealthHubAuthenticationContext,UserRole>, Repository<HealthHubAuthenticationContext,UserRole>>();
            #endregion

            #region BusinessRepos
            services.AddScoped<ITokenManager, TokenGeneration>();
            services.AddScoped<IUserRepo, Authentication_Repos.Contracts.UserRepo>();
            services.AddScoped<IUserRoles, UserRoles>();
            #endregion

            #region Authentication
            services.AddHttpContextAccessor();
            #endregion

            return services;
        }
    }
}
