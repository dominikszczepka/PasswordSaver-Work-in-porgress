using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public class UserServices : IUserServices
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        public UserServices(PassSaverDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;   
        }

        public UserDto GetCurrentUser(int id)
        {
            var currentUser = _dbContext
            .Users
            .Include(p => p.Passwords)
            .FirstOrDefault(p => p.Id == id);

            var userDto = _mapper.Map<UserDto>(currentUser);
            return userDto;
        }
    }
}
