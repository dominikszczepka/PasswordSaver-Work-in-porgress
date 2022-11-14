using Microsoft.EntityFrameworkCore;
using PassSaver.Models;

namespace PassSaver.Services
{
    public interface IUserServices
    {
        public UserDto GetCurrentUser(int id);
    }
}
