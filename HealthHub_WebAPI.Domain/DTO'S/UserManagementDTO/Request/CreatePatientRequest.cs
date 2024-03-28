using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request
{
    public class CreatePatientRequest
    {
        public byte[] Id { get; set; } = null!;

        public string UserName { get; set; } = null!;


        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string MiddleName { get; set; } = null!;

        public byte[] AddressId { get; set; } = null!;

        public byte[]? ReportTo { get; set; }

        public byte[]? ParentUserId { get; set; }

        public short Type { get; set; }

        public byte[]? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Dob { get; set; } = null!;

        public short Gender { get; set; }

        public string PhoneNumber { get; set; } = null!;

        public byte[]? RoleId { get; set; }

        public byte[]? DepartmentId { get; set; }

    }
}
