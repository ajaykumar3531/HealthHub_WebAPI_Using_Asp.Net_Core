using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Response
{
    public class CreateApmtResponse : StatusDTO
    {
        public string DoctorUserID { get; set; }
        public string DoctorName { get; set; }
    }

    public class DoctorApmts :StatusDTO
    {
        public List<DoctorDetails> DoctorDetails = new List<DoctorDetails>();
    }

    public class DoctorDetails : StatusDTO
    {
        public string DoctorID { get; set; }
        public string PatientID { get; set; }   
        public Int16 StartTime { get; set; }
        public Int16 EndTime { get; set; }
    }
}
