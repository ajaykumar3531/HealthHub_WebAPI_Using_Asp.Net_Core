using HealthHub_WebAPI.DAL.Authentication;
using HealthHub_WebAPI.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.BAL.Shared.JWT_Token
{
    public interface ITokenManager
    {
        Task<string> GenerateTokenAsync(LoginUserRequest request,int userId);
    }
}
