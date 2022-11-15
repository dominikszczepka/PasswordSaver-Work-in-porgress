using Microsoft.EntityFrameworkCore;
using PassSaver.Models;

namespace PassSaver.Services
{
    public interface IUserServices
    {
        public UserDto GetCurrentUser(int id);
        public int CreateUser(AddUserDto dto);
        public bool EditCurrentUser(int id, EditUserDto dto);
        public string IsUserOkToAdd(AddUserDto dto);
    }
}
