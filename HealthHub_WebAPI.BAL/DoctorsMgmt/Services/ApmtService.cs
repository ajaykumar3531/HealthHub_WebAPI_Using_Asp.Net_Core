using AutoMapper;
using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HealthHub_WebAPI.Domain.DTO.Common.Enums;

namespace HealthHub_WebAPI.BAL.DoctorsMgmt.Services
{
    public class ApmtService : IApmtService
    {
        private readonly IRepository<HelathHubDbContext, Appointment> _appointment;
        private readonly IRepository<HelathHubDbContext, User> _user;
        private readonly IMapper _mapper;

        public ApmtService(IRepository<HelathHubDbContext, Appointment> appointment, IRepository<HelathHubDbContext, User> user, IMapper mapper)
        {
            _appointment = appointment;
            _user = user;
            _mapper = mapper;
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
                    Status = (short)ApmtStatusEnum.Scheduled,
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
                                         Date = data.Date,
                                         StatusCode = StatusCodes.Status200OK,
                                         StatusMessage = Constants.MSG_DATA_LOAD_SUC
                                     }).ToList();

                    if (doctorDetails != null && doctorDetails.Any())
                    {
                        response.DoctorDetails.AddRange(doctorDetails);
                        response.StatusCode = StatusCodes.Status200OK;
                        response.StatusMessage = Constants.MSG_DATA_LOAD_SUC;
                    }
                    else
                    {
                        response.DoctorDetails.Add(new DoctorDetails
                        {
                            StatusCode = StatusCodes.Status400BadRequest,
                            StatusMessage = Constants.MSG_DATA_LOAD_FAIL
                        });
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
                if (response != null)
                    response = null;
            }
        }

        public async Task<UserApmtsResponse> GetUserApmts(string UserID)
        {
            UserApmtsResponse response = new UserApmtsResponse();
            List<Appointment> Apmts = new List<Appointment>();

            try
            {
                if (UserID == null)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    return response;
                }

                Apmts = (await _appointment.GetAll(x => x.PatientUserId == new PFAID(UserID).UID)).ToList();

                if (Apmts.Any() && Apmts != null)
                {
                    var userApmts = (from apmts in Apmts
                                     select new ApmtDetails
                                     {
                                         PatientID = new PFAID(apmts.PatientUserId).ToString(),
                                         AppointmentID = new PFAID(apmts.AppointmentId).ToString(),
                                         Date = apmts.Date,
                                         DoctorID = new PFAID(apmts.DoctorUserId).ToString(),
                                         EndTime = apmts.EndTime,
                                         StartTime = apmts.StartTime,
                                     }).ToList();

                    if (userApmts.Any() && userApmts.Count > 0)
                    {
                        response.ApmtDetails = userApmts;
                        response.StatusMessage = Constants.MSG_DATA_LOAD_SUC;
                        response.StatusCode = StatusCodes.Status200OK;
                    }
                    else
                    {
                        response.StatusMessage = Constants.MSG_DATA_LOAD_FAIL;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                    }
                }
                else
                {
                    response.StatusMessage = Constants.MSG_DATA_LOAD_FAIL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
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
                Apmts = null;
            }
        }

        public async Task<ApmtDetails> DeleteApmt(string AppointmentID)
        {
            ApmtDetails response = new ApmtDetails();
            Appointment apmts = new Appointment();
            try
            {
                if (AppointmentID == null)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    return response;
                }

                apmts = (await _appointment.GetAll(x => x.AppointmentId == new PFAID(AppointmentID).UID)).FirstOrDefault();

                if (apmts != null)
                {
                    _appointment.Delete(apmts);

                    if (await _appointment.SaveChangesAsync() > 0)
                    {
                        response = new ApmtDetails()
                        {
                            AppointmentID = new PFAID(apmts.AppointmentId).ToString(),
                            Date = apmts.Date,
                            DoctorID = new PFAID(apmts.DoctorUserId).ToString(),
                            PatientID = new PFAID(apmts.PatientUserId).ToString(),
                            EndTime = apmts.EndTime,
                            StartTime = apmts.StartTime,
                            StatusCode = StatusCodes.Status200OK,
                            StatusMessage = Constants.MSG_DATA_DEL_SUC
                        };
                    }
                    else
                    {
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        response.StatusMessage = Constants.MSG_DATA_DEL_EXC;
                    }
                }
                else
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusMessage = Constants.MSG_DATA_DEL_EXC;
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
                apmts = null;
            }

        }
    }
}
