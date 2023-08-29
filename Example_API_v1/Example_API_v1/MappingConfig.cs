using AutoMapper;
using Example_API_v1.Models;
using Example_API_v1.Models.DTO;

namespace Example_API_v1
{
    public class MappingConfig: Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa,villaDto>().ReverseMap();
            CreateMap<Villa, villaCreateDto>().ReverseMap();
            CreateMap<Villa, villaUpdateDto>().ReverseMap();
        }
    }
}
