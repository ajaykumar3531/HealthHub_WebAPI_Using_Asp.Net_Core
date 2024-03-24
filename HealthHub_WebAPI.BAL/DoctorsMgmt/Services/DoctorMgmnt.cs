using AutoMapper;
using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;

namespace HealthHub_WebAPI.BAL.DoctorsMgmt.Services
{
    public class DoctorMgmnt : IDoctorMgmnt
    {
        private readonly IRepository<HelathHubDbContext, Appointment> _appointment;
        private readonly IRepository<HelathHubDbContext, User> _user;
        private readonly IMapper _mapper;

        public DoctorMgmnt(IRepository<HelathHubDbContext, Appointment> appointment, IMapper mapper, IRepository<HelathHubDbContext, User> user)
        {
            _appointment = appointment;
            _mapper = mapper;
            _user = user;
        }

        public async Task<CreateApmtResponse> CreateAppoitment(CreateApmtRequest request, string UserID)
        {
            CreateApmtResponse response = new CreateApmtResponse();
            Appointment newAppointment = new Appointment();
            User doctorData = new User();
            try
            {
                if (request == null && UserID == null)
                {
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                byte[] AppoimentID = new PFAID().UID;
                newAppointment = new Appointment()
                {
                    AppointmentId = AppoimentID,
                    Date = request.Date,
                    DoctorUserId = new PFAID(request.DoctorUserID).UID,
                    PatientUserId = new PFAID(UserID).UID,
                    Description = request.Description,
                    EndTime = request.EndTime,
                    StartTime = request.StartTime,
                    AppointmentType = request.AppointmentType,
                };
                _appointment.Add(newAppointment);
                if (await _appointment.SaveChangesAsync() > 0)
                {
                    doctorData = (await _user.GetAll(x => x.Id == new PFAID(newAppointment.DoctorUserId).UID)).FirstOrDefault();

                    response = new CreateApmtResponse()
                    {
                        DoctorUserID = new PFAID(newAppointment.DoctorUserId).ToString(),
                        DoctorName = doctorData.FirstName + " " + doctorData.MiddleName + " " + doctorData.LastName,
                        StatusCode = StatusCodes.Status200OK,
                        StatusMessage = Constants.MSG_APMT_SUCC
                    };
                }
                else
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusMessage = Constants.MSG_APMT_FAIL;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response = null;
                newAppointment = null;
                doctorData = null;
            }
        }

        public async Task<DoctorApmts> GetDoctorApmts(string DoctorUserID)
        {
            DoctorApmts response = new DoctorApmts();
            List<Appointment> appointment = new List<Appointment>();
            List<DoctorDetails> doctorDetails = new List<DoctorDetails>();
            try
            {
                if (DoctorUserID == null)
                {
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                appointment = (await _appointment.GetAll(x => x.DoctorUserId == new PFAID(DoctorUserID).UID)).ToList();

                if (appointment.Any() && appointment != null)
                {
                    doctorDetails = (from data in appointment
                                              select new DoctorDetails
                                              {
                                                  DoctorID = new PFAID(data.DoctorUserId).ToString(),
                                                  PatientID = new PFAID(data.PatientUserId).ToString(),
                                                  StartTime = data.StartTime,
                                                  EndTime = data.EndTime,
                                                  StatusCode = StatusCodes.Status200OK,
                                                  StatusMessage = Constants.MSG_DATA_LOAD_SUC

                                              }).ToList();


                    if (doctorDetails != null && doctorDetails.Any())
                    {
                        response.DoctorDetails.AddRange(doctorDetails);
                        response.StatusCode = StatusCodes.Status200OK;
                        response.StatusMessage = Constants.MSG_DATA_LOAD_SUC;
                    }
                }
                else
                {
                    response.DoctorDetails.Add(new DoctorDetails
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        StatusMessage = Constants.MSG_DATA_LOAD_FAIL
                    });
                }
                return response;
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
