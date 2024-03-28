using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request
{
    public class CreateDoctorRequest
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public short Type { get; set; }

        public string Dob { get; set; } = null!;

        public short Gender { get; set; }

        public string PhoneNumber { get; set; } = null!;


        public string? Specialty { get; set; }

        public string Country { get; set; } = null!;

        public string State { get; set; } = null!;

        public string City { get; set; } = null!;

        public string Street { get; set; } = null!;

        public string CountryCode { get; set; } = null!;

        public string StatePinCode { get; set; } = null!;

        public string LandMark { get; set; } = null!;

        public string Address1 { get; set; } = null!;

        public string Address2 { get; set; } = null!;

    }
}
