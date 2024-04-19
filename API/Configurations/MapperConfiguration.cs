using API.Dto.Responses;
using API.Entities;
using AutoMapper;

namespace API.Configurations {
    public class MapperConfiguration: Profile {
        public MapperConfiguration() {
            CreateMap<User, UserResponse>();
        }
    }
}