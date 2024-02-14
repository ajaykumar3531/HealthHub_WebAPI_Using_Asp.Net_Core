using HealthHub_WebAPI.BAL.Authentication_Repos.Services;
using HealthHub_WebAPI.BAL.Shared.JWT_Token;
using HealthHub_WebAPI.DAL.Authentication;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.Domain.DTO.Request;
using HealthHub_WebAPI.Domain.DTO.Response;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;


namespace HealthHub_WebAPI.BAL.Authentication_Repos.Contracts
{
    /// <summary>
    /// Repository for user authentication operations.
    /// </summary>
    public class UserRepo : IUserRepo
    {
        #region Fields

        private readonly IRepository<HealthHubAuthenticationContext, User> _AuthContext;
        private readonly ITokenManager _tokenManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new instance of UserRepo.
        /// </summary>
        /// <param name="authContext">The repository for authenticatison context.</param>
        /// <param name="tokenManager">The token manager for JWT token generation.</param>
        public UserRepo(IRepository<HealthHubAuthenticationContext, User> authContext, ITokenManager tokenManager)
        {
            _AuthContext = authContext;
            _tokenManager = tokenManager;
        }

        #endregion

        #region Register&Login

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="request">Registration request containing user details.</param>
        /// <returns>A task representing the asynchronous operation, returning the registration response.</returns>
        public async Task<RegUserResponse> RegisterUser(RegUserRequest request)
        {
            RegUserResponse response = new RegUserResponse();
            if (request == null)
            {
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusMessage = Constants.MSG_REQ_NULL;
                return response;
            }

            try
            {
                var userData = (await _AuthContext.GetAll(x => x.Username == request.Username && x.Email == request.Email)).FirstOrDefault();
                if (userData != null)
                {
                    response.StatusCode = StatusCodes.Status410Gone;
                    response.StatusMessage = Constants.MSG_USER_FAIL;
                    return response;
                }
                else
                {
                    var PassWordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                    var AddUser = new User()
                    {
                        Username = request.Username,
                        Email = request.Email,
                        IsActive = true,
                        PasswordHash = PassWordHash,
                    };
                    _AuthContext.Add(AddUser);
                    if (await _AuthContext.SaveChangesAsync() > 0)
                    {
                        var userAddedData = (await _AuthContext.GetAll(x => x.Username == request.Username && x.Email == request.Email)).FirstOrDefault();
                        response = new RegUserResponse()
                        {
                            UserId = userAddedData.UserId,
                            RegistrationDate = userAddedData.RegistrationDate,
                            LastLoginDate = userAddedData.LastLoginDate,
                            Username = request.Username,
                            Email = request.Email,
                            PasswordHash = request.Password,
                            IsActive = userAddedData.IsActive,
                        };
                    }
                }
                response.StatusCode = StatusCodes.Status200OK;
                response.StatusMessage = Constants.MSG_USER_ADD;
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="request">Login request containing user credentials.</param>
        /// <returns>A task representing the asynchronous operation, returning the login response.</returns>
        public async Task<LoginUserResponse> LoginUser(LoginUserRequest request)
        {
            LoginUserResponse response = new LoginUserResponse();
            if (request == null)
            {
                response.StatusCode = StatusCodes.Status204NoContent;
                response.StatusMessage = Constants.MSG_REQ_NULL;
                return response;
            }

            var userData = (await _AuthContext.GetAll(x => x.Email == request.Email && x.Username == request.Username)).FirstOrDefault();

            try
            {
                bool isPassword = BCrypt.Net.BCrypt.Verify(request.Password, userData.PasswordHash);

                if (userData != null && isPassword)
                {
                    string JWTToken = await _tokenManager.GenerateTokenAsync(request,userData.UserId);
                    response = new LoginUserResponse()
                    {
                        Email = request.Email,
                        Password = request.Password,
                        JWTToken = JWTToken,
                    };
                    response.StatusCode = StatusCodes.Status200OK;
                    response.StatusMessage = Constants.MSG_LOGIN_SUCC;
                    return response;
                }
                response.StatusCode = StatusCodes.Status400BadRequest;
                response.StatusMessage = Constants.MSG_LOGIN_FAIL;
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (response != null)
                    response.Dispose();
                response = null;
                userData = null;
            }
        }

        #endregion

        #region GetByID
        /// <summary>
        /// Retrieves user details by user ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, returning the user details response.</returns>
        public async Task<UserDeatilsResponse> GetUserById(int id)
        {
            UserDeatilsResponse response = new UserDeatilsResponse(); ;
            try
            {
                var userData = (await _AuthContext.GetAll(x => x.UserId == id)).FirstOrDefault();
                if (userData == null)
                {
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    response.StatusMessage = Constants.MSG_NO_DATA_FOUND;
                    return response;
                }
                else
                {
                    response = new UserDeatilsResponse()
                    {
                        UserId = userData.UserId,
                        Email = userData.Email,
                        Username = userData.Username,
                        RegistrationDate = userData.RegistrationDate
                    };
                    response.StatusCode = StatusCodes.Status200OK;
                    response.StatusMessage = Constants.MSG_DATA_FOUND;
                    return response;
                }
               
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (response != null) response.Dispose(); response = null;
            }
        }
        #endregion

        #region UpdateUser
        /// <summary>
        /// Updates user information asynchronously.
        /// </summary>
        /// <param name="request">The request containing the updated user information.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response to the update operation.</returns>
        public async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request)
        {
            // Initialize response object
            UpdateUserResponse response = null;

            // Check if request is null
            if (request == null)
            {
                // Create response with bad request status and error message
                response = new UpdateUserResponse
                {
                    StatusMessage = Constants.MSG_REQ_NULL,
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return response;
            }

            // Retrieve user data from the database based on provided username and email
            User userData = (await _AuthContext.GetAll(x => x.Username == request.Username && x.Email == request.Email)).FirstOrDefault();

            try
            {
                // Hash the password before updating
                string password = BCrypt.Net.BCrypt.HashPassword(request.Password);

                if (userData != null)
                {
                    // Update user data
                    userData = new User()
                    {
                        Username = request.Username,
                        Email = request.Email,
                        PasswordHash = password,
                    };
                    _AuthContext.Update(userData);

                    // Save changes to the database
                    if (await _AuthContext.SaveChangesAsync() > 0)
                    {
                        // Create response with updated user information and success status
                        response = new UpdateUserResponse()
                        {
                            UserId = userData.UserId,
                            Username = userData.Username,
                            Email = userData.Email,
                            PasswordHash = request.Password,
                            IsActive = userData.IsActive,
                            StatusMessage = Constants.MSG_DATA_UPDATE_SUC,
                            StatusCode = StatusCodes.Status200OK
                        };
                        return response;
                    }
                }

                // If user data is null or saving changes failed, create response with failure status and message
                response = new UpdateUserResponse
                {
                    StatusMessage = Constants.MSG_DATA_UPDATE_FAIL,
                    StatusCode = StatusCodes.Status400BadRequest
                };
                return response;
            }
            catch
            {
                // Rethrow any caught exceptions
                throw;
            }
            finally
            {
                // Dispose response and reset variables
                if (response != null)
                    response.Dispose();
                response = null;
                userData = null;
            }
        }
        #endregion


    }
}
