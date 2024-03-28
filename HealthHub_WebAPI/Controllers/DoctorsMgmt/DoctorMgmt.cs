using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.UserManagementDTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HealthHub_WebAPI.Controllers.DoctorsMgmt
{
    [Route("HealthHub/DoctorManagement")]
    [ApiController]
    public class DoctorMgmt : ControllerBase
    {
        private readonly IDoctorMgmnt _doctorMgmnt;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DoctorMgmt(IDoctorMgmnt doctorMgmnt, IHttpContextAccessor httpContextAccessor)
        {
            _doctorMgmnt = doctorMgmnt;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("Hub/GetDoctors")]
        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            DoctorsResponse response = new DoctorsResponse();

            try
            {
                response = await _doctorMgmnt.GetDoctors();

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


        [Route("Hub/GetDoctorsPatient")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorsPatient()
        {
            DoctorsPatientResponse response = new DoctorsPatientResponse(); 

            var UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;

            try
            {
                response = await _doctorMgmnt.GetDoctorsPatient(UserID);

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
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(response != null) response = null;
            }
        }
    }
}
