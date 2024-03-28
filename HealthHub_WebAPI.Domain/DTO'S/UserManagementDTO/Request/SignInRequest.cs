using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request
{
    public class SignInRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

