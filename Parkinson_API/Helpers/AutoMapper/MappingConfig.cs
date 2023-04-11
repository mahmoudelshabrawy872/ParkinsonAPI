using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Parkinson_Models;
using Parkinson_Models.Dto;

namespace Parkinson_API.Helpers.AutoMapper
{

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Test1, Test1Dto>().ReverseMap();
            CreateMap<UserDto, IdentityUser>().ReverseMap();
            CreateMap<RegistrationRequestDto, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<Doctor, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<IdentityUser, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<IdentityUser, PatientRegisterRequestDto>().ReverseMap();



        }
    }
}
