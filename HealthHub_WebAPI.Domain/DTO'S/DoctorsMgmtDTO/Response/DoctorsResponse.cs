using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.DTO_S.DoctorsMgmtDTO.Response
{
    public class DoctorsResponse : StatusDTO
    {
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
    public class Doctor 
    {
        public string UserName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public short Type { get; set; }

        public string Dob { get; set; } = null!;

        public short Gender { get; set; }


        public string PhoneNumber { get; set; } = null!;

        public string? Specialty { get; set; }
    }
}
