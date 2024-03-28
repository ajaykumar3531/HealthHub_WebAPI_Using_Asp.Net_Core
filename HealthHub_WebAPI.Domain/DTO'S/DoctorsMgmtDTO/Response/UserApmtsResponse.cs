using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response
{
    public class UserApmtsResponse : StatusDTO
    {
        public List<ApmtDetails> ApmtDetails { get; set; } = new List<ApmtDetails>();
    }
    public class ApmtDetails : StatusDTO
    {
        public string AppointmentID { get; set;}
        public string DoctorID { get; set; }
        public string PatientID { get; set; }

        public Int16 StartTime { get; set; }
        public Int16 EndTime { get; set; }
        public DateTime Date { get; set; }
    }
}
