using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Controllers.UserManagement
{
    /// <summary>
    /// Controller for handling user management operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUsers : ControllerBase
    {
        private readonly IUserManagement _UserManagement;

        #region Constructor

        /// <summary>
        /// Constructor for CreateUsers controller
        /// </summary>
        /// <param name="userManagement">User management service</param>
        public CreateUsers(IUserManagement userManagement)
        {
            _UserManagement = userManagement;
        }

        #endregion

        #region Endpoints

        /// <summary>
        /// Endpoint for creating a doctor
        /// </summary>
        /// <param name="request">Doctor creation request</param>
        /// <returns>Action result containing the result of the operation</returns>
        [Route("h1/CreateDoctor")]
        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorRequest request)
        {
            CreateDoctorResponse response = new CreateDoctorResponse();
            try
            {
                response = await _UserManagement.CreateDoctor(request);
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

        #endregion
    }
}
