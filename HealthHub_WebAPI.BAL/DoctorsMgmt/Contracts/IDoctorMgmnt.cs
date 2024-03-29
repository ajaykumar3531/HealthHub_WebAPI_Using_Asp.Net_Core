﻿using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response;
using HealthHub_WebAPI.Domain.DTO.DTO_S.UserManagementDTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts
{
    public interface IDoctorMgmnt
    {
        Task<DoctorsResponse> GetDoctors();
        Task<DoctorsPatientResponse> GetDoctorsPatient(string DoctorUserID);

    }
}
