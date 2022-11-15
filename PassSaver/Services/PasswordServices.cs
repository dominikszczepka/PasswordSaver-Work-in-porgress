using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public class PasswordServices : IPasswordServices
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        public PasswordServices(PassSaverDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public IEnumerable<PasswordDto> GetAllUsersPasswords(User currentUser)
        {
            var passwords = _dbContext.Passwords.Where(p => p.UserId == currentUser.Id).ToList();
            var passwordsDtos = _mapper.Map<List<PasswordDto>>(passwords);
            return passwordsDtos;
        }
        public int AddPassword(AddPasswordDto dto)
        {
            var password = _mapper.Map<Password>(dto);
            _dbContext.Add(password);
            _dbContext.SaveChanges();
            return password.Id;
        }
        public bool DeletePassword(int passId,int userId)
        {
            var result= _dbContext.Passwords.FirstOrDefault(p=>p.Id==passId&&p.UserId==userId);
            if (result==null)
                return false;
            _dbContext.Passwords.Remove(result);
            _dbContext.SaveChanges();
            return true;
        }
        public bool EditPassword(EditPasswordDto dto)
        {
            var password = _dbContext.Passwords.Where(p => p.UserId == dto.UserId).FirstOrDefault();
            if (password == null) return false;
            password.Address=dto.Address;
            password.S_Password=dto.S_Password;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
