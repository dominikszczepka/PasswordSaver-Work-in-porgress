using AutoMapper;
using PassSaver.Models;
using PassSaver.Entities;

namespace PassSaver
{
    public class PassSaverMappingProfile : Profile
    {
        public PassSaverMappingProfile()
        {
            CreateMap<Password, PasswordDto>();
            CreateMap<User, UserDto>();

            CreateMap<AddPasswordDto, Password>();

        }
    }
}
