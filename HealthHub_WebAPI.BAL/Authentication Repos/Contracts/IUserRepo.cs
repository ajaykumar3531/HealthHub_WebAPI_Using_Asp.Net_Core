using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.Authentication_Repos.Services
{
    /// <summary>
    /// Interface for user authentication repository.
    /// </summary>
    public interface IUserRepo
    {
        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration request containing user details.</param>
        /// <returns>A task representing the asynchronous operation, returning the registration response.</returns>
        Task<RegUserResponse> RegisterUser(RegUserRequest request);

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">Login request containing user credentials.</param>
        /// <returns>A task representing the asynchronous operation, returning the login response.</returns>
        Task<LoginUserResponse> LoginUser(LoginUserRequest request);

        /// <summary>
        /// Gets user details by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the user details response.</returns>
        Task<UserDeatilsResponse> GetUserById(int id);

        /// <summary>
        /// Updates user information asynchronously.
        /// </summary>
        /// <param name="request">The request containing the updated user information.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response to the update operation.</returns>
        Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request);

    }
}
