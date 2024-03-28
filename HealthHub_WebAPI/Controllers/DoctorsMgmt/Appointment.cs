using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.BAL.DoctorsMgmt.Services;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthHub_WebAPI.Controllers.DoctorsMgmt
{
    [Route("HealthHub/Appointment")]
    [ApiController]
    public class Appointment : ControllerBase
    {
        private readonly IApmtService _appointment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Appointment(IApmtService appointment, IHttpContextAccessor httpContextAccessor)
        {
            _appointment = appointment;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("Hub/CreateAppointment")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateApmtRequest request)
        {
            CreateApmtResponse response = new CreateApmtResponse();
            string UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;

            try
            {
                response = await _appointment.CreateAppoitment(request, UserID);

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

        [Route("Hub/GetDoctorApmts")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorApmts()
        {
            DoctorApmts response = new DoctorApmts();
            string DoctorUserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;
            try
            {
                response = await _appointment.GetDoctorApmts(DoctorUserID);

                if (response != null && response.StatusCode == StatusCodes.Status200OK)
                {
                    return Ok(response.DoctorDetails);
                }
                else if (response != null && !string.IsNullOrEmpty(response.StatusMessage))
                {
                    return Ok(response.DoctorDetails);
                }
                else
                {
                    return BadRequest(response.DoctorDetails);
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
        

        [Route("Hub/GetUserApmts")]
        [HttpGet]
        public async Task<IActionResult> GetUserApmts()
        {
            UserApmtsResponse response = new UserApmtsResponse();

            string UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;

            try
            {
                response = await _appointment.GetUserApmts(UserID);

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
                if (response != null) response = null;
            }
        }

        [Route("Hub/DeleteApmt")]
        [HttpDelete]
        public async Task<IActionResult> DeleteApmt(string AppointmentID)
        {
            ApmtDetails response = new ApmtDetails();

            try
            {
                response = await _appointment.DeleteApmt(AppointmentID);

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
                if (response != null) response = null;
            }
        }
    }
}
