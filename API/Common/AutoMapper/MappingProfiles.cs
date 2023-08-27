using API.Dtos;
using API.Entities;
using AutoMapper;

namespace API.Common.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
        }
    }
}
