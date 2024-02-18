using HealthHub_WebAPI.BAL.Authentication_Repos.Contracts;
using HealthHub_WebAPI.DAL.Authentication;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System.Linq;
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

        #region GetAllUserRoles
        /// <summary>
        /// Retrieves all roles associated with a user asynchronously.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a list of all roles associated with the user.</returns>
        public async Task<List<UserAllRolesResponse>> GetAllRoles(string userID)
        {
            List<UserAllRolesResponse> response = new List<UserAllRolesResponse>(); // Initialize the list

            if (string.IsNullOrEmpty(userID))
            {
                // If userID is null or empty, return a BadRequest response
                response.Add(new UserAllRolesResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    StatusMessage = Constants.MSG_REQ_NULL
                });
                return response;
            }

            try
            {
                // Retrieve user roles and roles
                var userRoles = await _userRoleRepo.GetAll();
                var roles = await _roleRepo.GetAll();

                // Perform a join operation to get roles associated with the user
                var result = from userRole in userRoles
                             join role in roles
                             on userRole.RoleId equals role.RoleId
                             where userRole.UserId == int.Parse(userID)
                             select new UserAllRolesResponse
                             {
                                 userID = int.Parse(userID),
                                 RoleID = role.RoleId,
                                 RoleName = role.RoleName,
                                 RoleDescription = role.Description,
                                 StatusCode = StatusCodes.Status200OK,
                                 StatusMessage = Constants.MSG_DATA_FOUND
                             };

                // If roles are found, add them to the response list
                if (result != null && result.Any())
                {
                    response.AddRange(result);
                }
                else
                {
                    // If no roles are found, return a NotFound response
                    response.Add(new UserAllRolesResponse
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        StatusMessage = Constants.MSG_NO_DATA_FOUND,
                    });
                }
                return response;
            }
            catch
            {
                throw; // Rethrow the exception if needed
            }
            finally
            {
                // Ensure to clean up the response list
                if (response != null)
                    response = null;
            }
        }
        #endregion

    }

}

