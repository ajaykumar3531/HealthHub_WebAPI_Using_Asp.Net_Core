using HealthHub_WebAPI.BAL.Authentication_Repos.Contracts;
using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Controllers.Authentication
{
    [Route("HealthHub/Roles")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        #region Private Fields

        private readonly IUserRoles _userRole;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Constructor

        public UserRolesController(IUserRoles userRole, IHttpContextAccessor httpContextAccessor)
        {
            _userRole = userRole;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region CreateUserRoles

        /// <summary>
        /// Creates a new user role.
        /// </summary>
        /// <param name="request">The request containing the role information.</param>
        /// <returns>An asynchronous task representing the operation and containing the response to the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserRole(UserRoleRequest request)
        {
            UserRoleResponse response = new UserRoleResponse();
            // Get the UserID from the HttpContext User's claims
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;

            try
            {
                // Call the service to create user role
                response = await _userRole.CreateUserRole(request, userID);

                // Check the response and return appropriate IActionResult
                if (response != null && response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response);
                }
                else if (response != null && !string.IsNullOrEmpty(response.StatusMessage))
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch
            {
                // Rethrow any caught exceptions
                throw;
            }
            finally
            {
                // Dispose response and reset variable
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }

        #endregion

        #region GetAllUserRoles
        /// <summary>
        /// Retrieves all roles associated with the currently authenticated user.
        /// </summary>
        /// <returns>An asynchronous action result representing the operation's status and, if successful, a list of all roles associated with the user.</returns>
        [Route("GetAllUserRoles")]
        [HttpGet]
        public async Task<IActionResult> GetAllUserRoles()
        {
            List<UserAllRolesResponse> response = new List<UserAllRolesResponse>(); // Initialize the response list
            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value; // Retrieve the user ID of the currently authenticated user
            try
            {
                response = await _userRole.GetAllRoles(userID); // Retrieve all roles associated with the user
                if (response != null)
                {
                    // If roles are found, return a 200 OK response with the roles
                    return Ok(response);
                }
                // If no roles are found, return a 400 Bad Request response
                return BadRequest(response);
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
