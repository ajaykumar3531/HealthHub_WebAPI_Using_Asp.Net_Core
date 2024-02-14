using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.Request
{
    public class UserRoleRequest
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
}
