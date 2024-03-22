using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Controllers.UserManagement
{
    /// <summary>
    /// Controller for handling user management operations
    /// </summary>
    [Route("HealthHub/UserManagement")]
    [ApiController]
    public class UserMgmt : ControllerBase
    {
        private readonly IUserManagement _UserManagement;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #region Constructor

        /// <summary>
        /// Constructor for CreateUsers controller
        /// </summary>
        /// <param name="userManagement">User management service</param>
        public UserMgmt(IUserManagement userManagement,IHttpContextAccessor httpContextAccessor)
        {
            _UserManagement = userManagement;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Endpoint for creating a doctor
        /// </summary>
        /// <param name="request">Doctor creation request</param>
        /// <returns>Action result containing the result of the operation</returns>
        [Route("Hub/CreateDoctor")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorRequest request)
        {
            CreateDoctorResponse response = new CreateDoctorResponse();

            var userID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;

            try
            {
                response = await _UserManagement.CreateDoctor(request,userID);

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
                throw ex;
            }
            finally
            {
                if (response != null)
                    response = null;
            }
        }

        [Route("Hub/DeleteUser")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(DeleteUserRequest request)
        {
            DeleteUserResponse response = new DeleteUserResponse();
            try
            {
                response = await _UserManagement.Deleteuser(request);

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
            catch(Exception ex) {
                throw ex;
            }
            finally
            {

            }
        }
        #endregion
    }
}
