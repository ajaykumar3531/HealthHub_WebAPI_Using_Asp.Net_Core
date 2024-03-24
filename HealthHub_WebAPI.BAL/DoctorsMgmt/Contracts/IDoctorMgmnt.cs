using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request;
using HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.DoctorsMgmt.Contracts
{
    public interface IDoctorMgmnt
    {
        Task<CreateApmtResponse> CreateAppoitment(CreateApmtRequest request,string UserID);
        Task<DoctorApmts> GetDoctorApmts(string DoctorUserID);
    }
}
