using AutoMapper;
using Parkinson_Models;
using Parkinson_Models.Dto;

namespace Parkinson_API.Helpers.AutoMapper
{

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Test1, Test1Dto>().ReverseMap();
        }
    }
}
