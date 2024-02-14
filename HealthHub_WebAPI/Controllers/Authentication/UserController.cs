using HealthHub_WebAPI.BAL.Authentication_Repos.Services;
using HealthHub_WebAPI.BAL.Shared;
using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Controllers.Authentication
{
    /// <summary>
    /// Controller for user-related operations.
    /// </summary>
    [Route("HealthHub/User")]
    [ApiController]
    [ValidateModel]
    public class UserController : ControllerBase
    {

        #region Fields

        private readonly IUserRepo _user;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of UserController.
        /// </summary>
        /// <param name="user">The user repository.</param>
        public UserController(IUserRepo user)
        {
            _user = user;
        }

        #endregion

        #region Reg&Login

        /// <summary>
        /// Endpoint for registering a new user.
        /// </summary>
        /// <param name="request">The registration request.</param>
        /// <returns>The registration response.</returns>
        [Route("RegisterUser")]
        [HttpPost]
        public async Task<IActionResult> RegUser(RegUserRequest request)
        {
            RegUserResponse response = null;
            try
            {
                response = await _user.RegisterUser(request);
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
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }

        /// <summary>
        /// Endpoint for logging in a user.
        /// </summary>
        /// <param name="request">The login request.</param>
        /// <returns>The login response.</returns>
        [Route("LoginUser")]
        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginUserRequest request)
        {
            LoginUserResponse response = null;
            try
            {
                response = await _user.LoginUser(request);
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
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }

        #endregion

        #region GetById

        /// <summary>
        /// Retrieves user details by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        [Route("GetByUserId")]
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            UserDeatilsResponse response = null;
            try
            {
                response = await _user.GetUserById(id);
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
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// Handles HTTP POST requests to update user information.
        /// </summary>
        /// <param name="request">Update user request object.</param>
        /// <returns>An asynchronous task representing the operation and containing the update user response.</returns>
        [Route("UpdateUser")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserRequest request)
        {
            UpdateUserResponse response = null;
            try
            {
                response = await _user.UpdateUser(request);
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
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }
        #endregion

    }
}
