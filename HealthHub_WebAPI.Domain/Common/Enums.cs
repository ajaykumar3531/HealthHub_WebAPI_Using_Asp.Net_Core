using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.Common
{
    public class Enums
    {
        public enum DeleteStatusEnum
        {
            NotDeleted = 0,
            Deleted = 1,
        }

        public enum TypeEnum
        {
            Patient=1,
            Doctor = 2,
            Management = 3,
        }


        public enum ApmtStatusEnum
        {
            Scheduled = 1,
            Cancelled  = 2,
            Completed = 3,
        }
    }
}
