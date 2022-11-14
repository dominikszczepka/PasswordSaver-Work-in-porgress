using AutoMapper;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public interface IPasswordServices
    {
        public IEnumerable<PasswordDto> GetAllUsersPasswords(User currentUser);
        public int AddPassword(AddPasswordDto dto);
        
    }
}
