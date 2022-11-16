using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public class UserServices : IUserServices
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServices> _logger;
        public UserServices(PassSaverDbContext dbContext, IMapper mapper, ILogger<UserServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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
        public int CreateUser(AddUserDto dto)
        {
            _logger.LogInformation($"User {dto.Username} created");
            var user = _mapper.Map<User>(dto);
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
        public bool EditCurrentUser(int id, EditUserDto dto)
        {
            _logger.LogInformation($"User with id{id} was edited");
            var user= _dbContext.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null) return false;
            user.UserEmail = dto.UserEmail;
            user.UserHashedPassword = dto.UserHashedPassword;

            _dbContext.SaveChanges();
            return true;
        }
        public string AreUserCredentialsInDB(AddUserDto dto)
        {
            var duplicateUsers = _dbContext.Users.Where(u => u.Username == dto.Username);
            if (duplicateUsers != null) return "Username already exists";
            duplicateUsers = _dbContext.Users.Where(u => u.UserEmail == dto.UserEmail);
            if (duplicateUsers != null) return "Email already exists";
            return null;
        }
    }
}
