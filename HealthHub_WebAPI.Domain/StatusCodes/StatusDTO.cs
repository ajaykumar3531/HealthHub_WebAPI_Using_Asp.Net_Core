using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.StatusCodes
{
    public class StatusDTO
    {
        public int? StatusCode { get; set; }
        public string? StatusMessage { get; set; }
    }

}
