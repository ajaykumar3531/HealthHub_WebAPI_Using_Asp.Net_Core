using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.DoctorsMgmtDTO.Request
{
    public class CreateApmtRequest
    {
        public string DoctorUserID { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public short Status { get; set; }
        public short AppointmentType { get; set; }
        public short StartTime { get; set; }
        public short EndTime { get; set; }
    }
}
