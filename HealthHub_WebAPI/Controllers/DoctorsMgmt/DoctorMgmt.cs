using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
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

        [Route("Hub/CreateAppointment")]
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(CreateApmtRequest request)
        {
            CreateApmtResponse response = new CreateApmtResponse();
            string UserID = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Actor).Value;
            try
            {
                response = await _doctorMgmnt.CreateAppoitment(request, UserID);

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
        public async Task<IActionResult> GetDoctorApmts(string DoctorUserID)
        {
            DoctorApmts response = new DoctorApmts();

            try
            {
                response = await _doctorMgmnt.GetDoctorApmts(DoctorUserID);

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

            }
        }
    }
}
