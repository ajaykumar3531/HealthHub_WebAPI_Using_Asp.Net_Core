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
        /// <summary>
        /// Creates a new user role asynchronously.
        /// </summary>
        /// <param name="request">The request containing the role information.</param>
        /// <param name="userID">The ID of the user associated with the role.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response to the creation operation.</returns>
        Task<UserRoleResponse> CreateUserRole(UserRoleRequest request, string userID);

    }
}
