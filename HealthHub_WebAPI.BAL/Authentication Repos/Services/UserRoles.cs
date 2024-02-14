using HealthHub_WebAPI.BAL.Authentication_Repos.Contracts;
using HealthHub_WebAPI.DAL.Authentication;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;

namespace HealthHub_WebAPI.BAL.Authentication_Repos.Services
{
    public class UserRoles : IUserRoles
    {
        #region Private Fields

        private readonly IRepository<HealthHubAuthenticationContext, Role> _roleRepo;
        private readonly IRepository<HealthHubAuthenticationContext, UserRole> _userRoleRepo;

        #endregion

        #region Constructor

        public UserRoles(IRepository<HealthHubAuthenticationContext, Role> roleRepo, IRepository<HealthHubAuthenticationContext, UserRole> userRoleRepo)
        {
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
        }

        #endregion

        #region CreateUserRoles

        /// <summary>
        /// Creates a new user role.
        /// </summary>
        /// <param name="request">The request containing the role information.</param>
        /// <param name="userID">The ID of the user associated with the role.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response to the creation operation.</returns>
        public async Task<UserRoleResponse> CreateUserRole(UserRoleRequest request, string userID)
        {
            UserRoleResponse response = new UserRoleResponse();
            Role role = new Role();
            UserRole userRole = new UserRole();

            // Check if request is null
            if (request == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusMessage = Constants.MSG_REQ_NULL;
                return response;
            }

            // Retrieve role from repository based on provided role name and description
            role = (await _roleRepo.GetAll(x => x.RoleName.ToLower() == request.RoleName.ToLower() && x.Description.ToLower() == request.RoleDescription.ToLower())).FirstOrDefault();

            try
            {
                // If role does not exist, create new role
                if (role == null)
                {
                    role = new Role()
                    {
                        RoleName = request.RoleName,
                        Description = request.RoleDescription,
                    };
                    _roleRepo.Add(role);

                    // Save changes to role repository
                    if (await _roleRepo.SaveChangesAsync() > 0)
                    {
                        userRole = new UserRole()
                        {
                            RoleId = role.RoleId,
                            UserId = int.Parse(userID)
                        };
                        _userRoleRepo.Add(userRole);
                        await _userRoleRepo.SaveChangesAsync();
                    }

                    // Create response with success status and message
                    response = new UserRoleResponse()
                    {
                        RoleId = role.RoleId,
                        RoleName = request.RoleName,
                        RoleDescription = request.RoleDescription,
                    };
                    response.StatusCode = StatusCodes.Status200OK;
                    response.StatusMessage = Constants.MSG_DATA_ADD_SUC;
                    return response;
                }

                // If role already exists, create response with failure status and message
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusMessage = Constants.MSG_DATA_ADD_FAIL;
                return response;
            }
            catch
            {
                // Rethrow any caught exceptions
                throw;
            }
            finally
            {
                // Dispose response and reset variables
                if (response != null)
                    response.Dispose();
                response = null;
                role = null;
                userRole = null;
            }
        }

        #endregion
    }
}
