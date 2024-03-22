using AutoMapper;
using HealthHub_WebAPI.BAL.Shared.JWT_Token;
using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using static HealthHub_WebAPI.Domain.DTO.Common.Enums;

namespace HealthHub_WebAPI.BAL.User_Managemnt.Services
{
    public class UserManagement : IUserManagement
    {
        #region Contr
        private readonly IRepository<HelathHubDbContext, User> _userManagement;
        private readonly IRepository<HelathHubDbContext, Address> _address;
        private readonly IRepository<HelathHubDbContext, Role> _role;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenGenerate;

        public UserManagement(IRepository<HelathHubDbContext, User> userManagement, IRepository<HelathHubDbContext, Address> address, IRepository<HelathHubDbContext, Role> role, IMapper mapper,ITokenManager tokenManager)
        {
            _userManagement = userManagement;
            _address = address;
            _role = role;
            _mapper = mapper;
            _tokenGenerate = tokenManager;
        }
        #endregion

        /// <summary>
        /// Method to create a new doctor based on the provided request data.
        /// </summary>
        /// <param name="request">The request object containing doctor information.</param>
        /// <param name="SignInUserID">The user ID of the user performing the sign-in.</param>
        /// <returns>A response object indicating the status of the doctor creation process.</returns>
        public async Task<CreateDoctorResponse> CreateDoctor(CreateDoctorRequest request, string SignInUserID)
        {
            // Initialize variables
            User user = null;
            Address address = null;
            Role role = null;
            CreateDoctorResponse response = null;

            // Check if the request is null
            if (request == null)
            {
                // Set response status for a null request
                response.StatusMessage = Constants.MSG_REQ_NULL;
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;
            }

            try
            {
                // Check if the user already exists
                user = (await _userManagement.GetAll(x => x.UserName == request.UserName)).FirstOrDefault();

                if (user == null)
                {
                    // Generate unique IDs for role, address, and user
                    byte[] userID = new PFAID().UID;
                    byte[] addressId = new PFAID().UID;
                    byte[] roleId = new PFAID().UID;
                    byte[] signInUserID = new PFAID(SignInUserID).UID;

                    // Hash the password
                    var PassWordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    // Create a new role
                    role = new Role()
                    {
                        Id = roleId,
                        CreatedBy = signInUserID,
                        ModifiedBy = signInUserID,
                        RoleName = request.Specialty,
                    };
                    _role.Add(role);

                    // Create a new address
                    address = new Address()
                    {
                        Id = addressId,
                    };
                    _mapper.Map(request, address);
                    _address.Add(address);

                    // Create a new user
                    user = new User()
                    {
                        Id = userID,
                        AddressId = address.Id,
                        RoleId = role.Id
                    };
                    _mapper.Map(request, user);
                    user.Password = PassWordHash;
                    _userManagement.Add(user);

                    // Save changes asynchronously
                    if (await _userManagement.SaveChangesAsync() > 0)
                    {
                        // Set response for successful creation
                        response = new CreateDoctorResponse()
                        {
                            Id = new PFAID(user.Id).ToString(),
                            Type = user.Type,
                            UserName = user.UserName,
                            StatusMessage = Constants.MSG_DATA_ADD_SUC,
                            StatusCode = StatusCodes.Status200OK,
                        };
                    }
                    else
                    {
                        // Set response for failed creation
                        response.StatusMessage = Constants.MSG_DATA_ADD_FAIL;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        return response;
                    }
                }
                return response;

            }
            catch (Exception ex)
            {
                // Throw any exceptions encountered
                throw ex;
            }
            finally
            {
                // Clean up resources
                if (response != null)
                    response = null;
                user = null;
            }
        }

        /// <summary>
        /// Method to sign in a user with the provided credentials.
        /// </summary>
        /// <param name="request">The request object containing user sign-in credentials.</param>
        /// <returns>A response object indicating the status of the sign-in process.</returns>
        public async Task<SignInResponse> SignInUser(SignInRequest request)
        {
            // Initialize variables
            SignInResponse response = new SignInResponse();
            User UserData = new User();

            try
            {
                // Check if the request is null
                if (request == null)
                {
                    response.StatusCode = StatusCodes.Status204NoContent;
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    return response;
                }

                // Retrieve user data based on username
                UserData = (await _userManagement.GetAll(x => x.UserName == request.UserName)).FirstOrDefault();

                // Verify password
                bool isPassword = BCrypt.Net.BCrypt.Verify(request.Password, UserData.Password);

                // Check if user exists and password is correct
                if (UserData != null && isPassword)
                {
                    // Generate JWT token
                    string JWTToken = await _tokenGenerate.GenerateTokenAsync(request, new PFAID(UserData.Id).ToString());

                    // Check if token generation is successful
                    if (JWTToken != null)
                    {
                        // Set response for successful sign-in
                        response = new SignInResponse()
                        {
                            UserName = UserData.UserName,
                            JWTToken = JWTToken,
                            StatusCode = StatusCodes.Status200OK,
                            StatusMessage = Constants.MSG_LOGIN_SUCC
                        };
                    }
                }
                else
                {
                    // Set response for failed sign-in
                    response.StatusMessage = Constants.MSG_LOGIN_FAIL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                return response;
            }
            catch (Exception ex)
            {
                // Throw any exceptions encountered
                throw ex;
            }
            finally
            {
                // Clean up resources
                if (response != null)
                    response = null;
                UserData = null;
            }
        }

        public async Task<DeleteUserResponse> Deleteuser(DeleteUserRequest request)
        {
            User userData = new User();
            DeleteUserResponse response = new DeleteUserResponse();
            try
            {
                // Check if the request is null
                if (request == null)
                {
                    // Set response status for a null request
                    response.StatusMessage = Constants.MSG_REQ_NULL;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                userData = (await _userManagement.GetAll(x => x.UserName == request.UserName )).FirstOrDefault();

                bool isPassword = BCrypt.Net.BCrypt.Verify(request.Password,userData.Password);

                if(userData != null && isPassword)
                {
                    userData.Status = (int)DeleteStatusEnum.Deleted;
                    _userManagement.Update(userData);
                    if (await _userManagement.SaveChangesAsync() > 0)
                    {
                        response = new DeleteUserResponse()
                        {
                            UserId = new PFAID(userData.Id).ToString(),
                            UserName = userData.UserName,
                            StatusMessage = Constants.MSG_DATA_DEL_SUC,
                            StatusCode = StatusCodes.Status200OK,
                        };
                    }
                }
                else
                {
                    response.StatusMessage = Constants.MSG_DATA_DEL_EXC;
                    response.StatusCode = StatusCodes.Status400BadRequest;
                }
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response = null;
                userData = null;
            }
        }
    }
}
