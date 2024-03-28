using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.StatusCodes
{
    public static class  Constants
    {
        #region Common Messages
        public const string MSG_NO_DATA_FOUND = "No data found";
        public const string MSG_DATA_FOUND = "Data found";
        public const string MSG_SUCCESS = "Success";
        public const string MSG_ERROR = "Error Occurred";
        public const string MSG_ENDPOINT_ERROR = "There is problem executing current end point";
        public const string MSG_JE_COM_STAT = "Something went wrong please check the information.";
        public const string MSG_FAILED = "Failed";

        public const string MSG_DATA_DEL_SUC = "Data deleted Successfully";
        public const string MSG_DATA_DEL_EXC = "Failed to delete data";

        public const string MSG_DATA_LOAD_SUC = "Data fetched successfully";
        public const string MSG_DATA_LOAD_FAIL = "Failed to fetch data";

        public const string MSG_DATA_ADD_SUC = "Data added Successfully";
        public const string MSG_DATA_ADD_FAIL = "Failed to add data";

        public const string MSG_DATA_UPDATE_SUC = "Data updated Successfully";
        public const string MSG_DATA_UPDATE_FAIL = "Failed to update data";

        public const string MSG_DAYS_EXCEED = "Number of days cannot be greater than 365";
        public const string MSG_ACC_ARCH = "Account Archived ";
        public const string MSG_ACC_ARCH_FAIL = "Failed to Archived Account ";
        public const string MSG_ARC_ACT_SUC = "Account moved to active ";
        public const string MSG_LOGIN_SUCC = "Login Successfull";
        public const string MSG_LOGIN_FAIL = "Login Faill";
        public const string MSG_USER_ADD = "User added Successfully";
        public const string MSG_USER_FAIL = "User added failed";
        public const string MSG_REQ_NULL = "The request is null";
        public const string MSG_APMT_SUCC = "Doctor Appointment Scheduled Successfull";
        public const string MSG_APMT_FAIL = "Doctor Appointment Not Scheduled";
        #endregion

    }
}

