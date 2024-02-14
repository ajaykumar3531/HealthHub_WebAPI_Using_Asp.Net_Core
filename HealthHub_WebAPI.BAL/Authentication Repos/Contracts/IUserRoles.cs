using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.Authentication_Repos.Contracts
{
    public interface IUserRoles
    {
        Task<UserRoleResponse> CreateUserRole(UserRoleRequest request,string userID);
    }
}
