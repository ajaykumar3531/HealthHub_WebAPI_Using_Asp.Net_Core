using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace HealthHub_WebAPI.Controllers.UserManagement
{
    /// <summary>
    /// Controller for handling user sign-in operations.
    /// </summary>
    [Route("HealthHub/SignInUsers")]
    [ApiController]
    public class SignInUserController : ControllerBase
    {
        private readonly IUserManagement _userManagement;

        /// <summary>
        /// Constructor for SignInUserController.
        /// </summary>
        /// <param name="userManagement">The user management service.</param>
        public SignInUserController(IUserManagement userManagement)
        {
            _userManagement = userManagement;
        }

        #region Sign-In User Endpoint

        /// <summary>
        /// Endpoint for signing in a user.
        /// </summary>
        /// <param name="request">The sign-in request object.</param>
        /// <returns>An IActionResult representing the result of the sign-in operation.</returns>
        [Route("Hub/SignInUser")]
        [HttpPost]
        public async Task<IActionResult> SignInUsers(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();

            try
            {
                // Call user management service to sign in the user
                response = await _userManagement.SignInUser(request);

                // Check response status and return appropriate IActionResult
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
            catch (Exception ex)
            {
                // Throw any exceptions encountered during sign-in
                throw ex;
            }
            finally
            {
                // Clean up resources
                if (response != null)
                    response = null;
            }
        }

        #endregion
    }
}
