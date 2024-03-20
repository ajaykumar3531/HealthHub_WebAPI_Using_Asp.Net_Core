using AutoMapper;
using HealthHub_WebAPI.DAL.HelathHub;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Request;
using HealthHub_WebAPI.Domain.DTO.UserManagementDTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.Common
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, CreateDoctorRequest>().ReverseMap();
            CreateMap<Address, CreateDoctorRequest>().ReverseMap();
            CreateMap<Role, CreateDoctorRequest>().ReverseMap();
        }
    }
}
