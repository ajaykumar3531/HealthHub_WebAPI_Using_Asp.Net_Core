using AutoMapper;
using HealthHub_WebAPI.BAL.User_Managemnt.Contracts;
using HealthHub_WebAPI.DAL.Generic_Repos;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.Common;
using HealthHub_WebAPI.Domain.DTO.StatusCodes;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using System;
using System.Text;

namespace HealthHub_WebAPI.BAL.User_Managemnt.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly IRepository<HelathHubDbContext, User> _userManagement;
        private readonly IRepository<HelathHubDbContext, Address> _address;
        private readonly IRepository<HelathHubDbContext, Role> _role;
        private readonly IRepository<HelathHubDbContext, Department> _dept;
        private readonly IMapper _mapper;

        public UserManagement(IRepository<HelathHubDbContext, User> userManagement, IRepository<HelathHubDbContext, Address> address, IRepository<HelathHubDbContext, Role> role, IRepository<HelathHubDbContext, Department> dept, IMapper mapper)
        {
            _userManagement = userManagement;
            _address = address;
            _role = role;
            _dept = dept;
            _mapper = mapper;
        }

        public async Task<CreateDoctorResponse> CreateDoctor(CreateDoctorRequest request)
        {
            User user = null;
            Address address = null;
            Role role = null;
            CreateDoctorResponse response = null;
            if (request == null)
            {
                response.StatusMessage = Constants.MSG_REQ_NULL;
                response.StatusCode = StatusCodes.Status400BadRequest;
                return response;
            }

            try
            {
                if (user == null)
                {
                    string userID = await GenerateID();
                    byte[] idBytes = HexStringToByteArray(userID.Substring(2));

                    string AddressID = await GenerateID();
                    byte[] addressIdBytes = HexStringToByteArray(AddressID.Substring(2));

                    string RoleID = await GenerateID();
                    byte[] roleIdBytes = HexStringToByteArray(RoleID.Substring(2));

                    var PassWordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                    role = new Role()
                    {
                        Id = roleIdBytes,
                        CreatedBy = idBytes,
                        ModifiedBy = idBytes,
                        RoleName = "Doctor"
                    };
                    _role.Add(role);

                    address = new Address()
                    {
                        Id = addressIdBytes,

                    };
                    _mapper.Map(request, address);
                    _address.Add(address);
          
                    user = new User()
                    {
                        Id = idBytes,
                        AddressId = address.Id,
                        RoleId = role.Id
                    };
                    _mapper.Map(request, user);
                    user.Password = PassWordHash;
                    _userManagement.Add(user);
                    if (await _userManagement.SaveChangesAsync() > 0)
                    {
                        response = new CreateDoctorResponse()
                        {
                            Id = user.Id,
                            Type = user.Type,
                            UserName = user.UserName,
                            StatusMessage = Constants.MSG_DATA_ADD_SUC,
                            StatusCode = StatusCodes.Status200OK,
                        };
                    }
                    else
                    {
                        response.StatusMessage = Constants.MSG_DATA_ADD_FAIL;
                        response.StatusCode = StatusCodes.Status400BadRequest;
                        return response;
                    }
                }
                return response;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (response != null)
                    response = null;
                user = null;
            }
        }


        public async Task<string> GenerateID()
        {
            // Generate a new GUID
            Guid guid = Guid.NewGuid();

            // Convert the GUID to a byte array
            byte[] byteArray = guid.ToByteArray();

            // Format the byte array as a hexadecimal string
            string userId = "0x" + BitConverter.ToString(byteArray).Replace("-", "").PadRight(32, '0');

            return userId;
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            int length = hex.Length;
            byte[] bytes = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

    }
}
