using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;

namespace HealthHub_WebAPI.BAL.User_Managemnt.Contracts
{
    public interface IUserManagement
    {
        Task<CreateDoctorResponse> CreateUser(CreateDoctorRequest request,string UserID);
        Task<SignInResponse> SignInUser(SignInRequest request);
        Task<DeleteUserResponse> Deleteuser(DeleteUserRequest request);
    }

}
