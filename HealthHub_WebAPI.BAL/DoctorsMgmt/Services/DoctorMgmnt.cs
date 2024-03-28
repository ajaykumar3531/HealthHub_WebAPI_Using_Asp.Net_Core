using AutoMapper;
using HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.UserManagementDTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using static HealthHub_WebAPI.Domain.DTO.Common.Enums;

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

        public async Task<DoctorsResponse> GetDoctors()
        {
            List<User> users = new List<User>();

            DoctorsResponse response = new DoctorsResponse();

            try
            {
                users = (await _user.GetAll(x => x.Type == (short)TypeEnum.Doctor && x.Status == (short)DeleteStatusEnum.NotDeleted)).ToList();

                if (users.Any() && users != null)
                {
                    var Doctors = (from user in users
                                   select new Doctor
                                   {
                                       UserName = user.UserName,
                                       Type = user.Type,
                                       Dob = user.Dob,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       MiddleName = user.MiddleName,
                                       Gender = user.Gender,
                                       PhoneNumber = user.PhoneNumber,
                                       Specialty = user.Specialty,
                                   }).ToList();

                    if (Doctors.Any() && Doctors != null)
                    {
                        response.Doctors.AddRange(Doctors);
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
                users = null;
            }
        }


        public async Task<DoctorsPatientResponse> GetDoctorsPatient(string DoctorUserID)
        {
            DoctorsPatientResponse response = new DoctorsPatientResponse();
            List<User> patients = new List<User>();
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                if (DoctorUserID == null)
                {
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                patients = (await _user.GetAll(x => x.Type == (short)TypeEnum.Patient && x.Status == (short)DeleteStatusEnum.NotDeleted)).ToList();
                appointments = (await _appointment.GetAll(x=>x.DoctorUserId == new PFAID(DoctorUserID).UID)).ToList();

                if (patients.Any() && patients != null && appointments.Any() && appointments != null)
                {
                    var result = (from apmt in appointments
                                  join patinet in patients on
                                  apmt.PatientUserId equals patinet.Id
                                  select new DoctorsPatient
                                  {
                                      Dob = patinet.Dob,
                                      FirstName = patinet.FirstName,
                                      LastName = patinet.LastName,
                                      Gender = patinet.Gender,
                                      MiddleName = patinet.MiddleName,
                                      PhoneNumber = patinet.PhoneNumber,
                                      Type = patinet.Type,
                                      UserName = patinet.UserName,
                                  }).ToList();

                    if (result.Any() && result != null)
                    {
                        response.Patients.AddRange(result);
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
                appointments = null;
                patients = null;
            }
        }
    }
}
