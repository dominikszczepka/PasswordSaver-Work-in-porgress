using AutoMapper;
using PassSaver.Models;
using PassSaver.Entities;

namespace PassSaver
{
    public class PassSaverMappingProfile : Profile
    {
        private readonly PasswordHasher _passwordHasher;
        public PassSaverMappingProfile()
        {
            _passwordHasher = new PasswordHasher();
            CreateMap<Password, PasswordDto>();
            CreateMap<User, UserDto>();

            CreateMap<AddPasswordDto, Password>();
            CreateMap<AddUserDto, User>()
                .ForMember(a => a.UserHashedPassword, b => b.MapFrom(c => _passwordHasher.Hash(c.UserUnhashedPassword)));

            CreateMap<AddUserDto, CheckIfUserExistsDto>();
        }
    }
}
