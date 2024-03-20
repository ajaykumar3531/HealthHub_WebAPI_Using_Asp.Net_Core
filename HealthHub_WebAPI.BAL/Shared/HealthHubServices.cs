using HealthHub_WebAPI.BAL.Shared.JWT_Token;
using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.BAL.User_Managemnt.Services;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
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
            services.AddDbContext<HelathHubDbContext>();
            #endregion

            #region Repos
            services.AddScoped<IRepository<HelathHubDbContext, User>, Repository<HelathHubDbContext, User>>();
            services.AddScoped<IRepository<HelathHubDbContext, Role>, Repository<HelathHubDbContext, Role>>();
            services.AddScoped<IRepository<HelathHubDbContext, Department>, Repository<HelathHubDbContext, Department>>();
            services.AddScoped<IRepository<HelathHubDbContext, Appointment>, Repository<HelathHubDbContext, Appointment>>();
            services.AddScoped<IRepository<HelathHubDbContext, Address>, Repository<HelathHubDbContext, Address>>();
            #endregion

            #region BusinessRepos
            services.AddScoped<ITokenManager, TokenGeneration>();
            services.AddScoped<IUserManagement,UserManagement>();
            #endregion

            #region Authentication
            services.AddHttpContextAccessor();
            #endregion

            #region Other
            #endregion

            return services;
        }
    }
}
