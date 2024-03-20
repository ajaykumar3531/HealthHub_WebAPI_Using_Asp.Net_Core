using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response
{
    public class CreatePatientResponse
    {
        public byte[] Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public short Type { get; set; }
    }
}
