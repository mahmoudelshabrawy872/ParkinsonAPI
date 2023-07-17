using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Parkinson_Models;
using Parkinson_Models.Dto;
using Parkinson_Models.Dto.ClickTestDtos;
using Parkinson_Models.Dto.MemoryTestDtos;
using Parkinson_Models.Dto.SpiralTestDtos;

namespace Parkinson_API.Helpers.AutoMapper
{

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Test, TestDto>().ReverseMap();
            CreateMap<UserDto, IdentityUser>().ReverseMap();
            CreateMap<RegistrationRequestDto, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<Doctor, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<IdentityUser, DoctorRegisterRequestDto>().ReverseMap();
            CreateMap<IdentityUser, PatientRegisterRequestDto>().ReverseMap();
            CreateMap<ClickTest, ClickTestDto>().ReverseMap();
            CreateMap<ClickTest, CreateClickTestDto>().ReverseMap();
            CreateMap<SpiralTest, SpiralTestDto>().ReverseMap();
            CreateMap<SpiralTest, CreateSpiralTestDto>().ReverseMap();
            CreateMap<MemoryTest, MemoryTestDto>().ReverseMap();
            CreateMap<MemoryTest, CreateMemoryTestDto>().ReverseMap();
            CreateMap<ReactionTest, ReactionTestDto>().ReverseMap();
            CreateMap<ReactionTest, CreateReactionTestDto>().ReverseMap();




        }
    }
}
