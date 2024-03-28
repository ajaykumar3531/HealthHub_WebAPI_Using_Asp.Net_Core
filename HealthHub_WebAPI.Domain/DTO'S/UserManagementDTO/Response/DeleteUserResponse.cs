using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response
{
    public class DeleteUserResponse : StatusDTO
    {
        public string UserId { get; set;}
        public string UserName { get; set;}
    }
}

