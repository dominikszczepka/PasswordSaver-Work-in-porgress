using AutoMapper;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public interface IPasswordServices
    {
        public IEnumerable<PasswordDto> GetAllUsersPasswords(User currentUser);
        public int AddPassword(AddPasswordDto dto);
        public bool DeletePassword(int passId, int userId);
        public bool EditPassword(EditPasswordDto dto);
    }
}
