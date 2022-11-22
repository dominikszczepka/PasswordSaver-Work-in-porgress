using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Exceptions;
using PassSaver.Models;

namespace PassSaver.Services
{
    public interface IUserServices
    {
        public UserDto GetCurrentUser(int id);
        public int CreateUser(AddUserDto dto);
        public void EditCurrentUser(int id, EditUserDto dto);
        public void AreUserCredentialsTaken(AddUserDto dto);
        public User IsUserInTheDB(CheckIfUserExistsDto dto);
    }
    public class UserServices : IUserServices
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServices> _logger;
        private readonly PasswordHasher _passHasher;
        public UserServices(PassSaverDbContext dbContext, IMapper mapper, ILogger<UserServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _passHasher = new PasswordHasher();
        }

        public UserDto GetCurrentUser(int id)
        {
            var currentUser = _dbContext
            .Users
            .Include(p => p.Passwords)
            .FirstOrDefault(p => p.Id == id);

            if (currentUser == null) throw new UserNotFoundException();
            var userDto = _mapper.Map<UserDto>(currentUser);
            return userDto;
        }
        public int CreateUser(AddUserDto dto)
        {
            IsUserInTheDB(_mapper.Map<CheckIfUserExistsDto>(dto));
            _logger.LogInformation($"User {dto.Username} created");
            var user = _mapper.Map<User>(dto);
            _dbContext.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }
        public void EditCurrentUser(int id, EditUserDto dto)
        {
            _logger.LogInformation($"User with id{id} was edited");
            var user= _dbContext.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null) throw new UserNotFoundException();
            user.UserEmail = dto.UserEmail;
            user.UserHashedPassword = _passHasher.Hash(dto.UserPassword);

            _dbContext.SaveChanges();
        }
        public void AreUserCredentialsTaken(AddUserDto dto)
        {
            var duplicateUsers = _dbContext.Users.Where(u => u.Username == dto.Username);
            if (duplicateUsers != null) throw new UserCredentialsTakenException("Username already exists");
            duplicateUsers = _dbContext.Users.Where(u => u.UserEmail == dto.UserEmail);
            if (duplicateUsers != null) throw new UserCredentialsTakenException("Email already exists");
        }
        public User IsUserInTheDB(CheckIfUserExistsDto dto)
        {
            var hashedDtoPassword = _passHasher.Hash(dto.UserUnhashedPassword);
            var matchingUser = _dbContext.Users.Where(u => u.Username == dto.Username && u.UserHashedPassword == hashedDtoPassword).FirstOrDefault();
            if (matchingUser == null) throw new UserNotFoundException();
            return matchingUser;
        }
    }
}
