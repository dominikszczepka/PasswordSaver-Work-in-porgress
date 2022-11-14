using AutoMapper;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Services
{
    public class PasswordServices : IPasswordServices
    {
        private readonly PassSaverDbContext _passwordDbContext;
        private readonly IMapper _mapper;
        public PasswordServices(PassSaverDbContext passwordDbContext, IMapper mapper)
        {
            _passwordDbContext = passwordDbContext;
            _mapper = mapper;
        }
        public IEnumerable<PasswordDto> GetAllUsersPasswords(User currentUser)
        {
            var passwords = _passwordDbContext.Passwords.Where(p => p.UserId == currentUser.Id).ToList();
            var passwordsDtos = _mapper.Map<List<PasswordDto>>(passwords);
            return passwordsDtos;
        }
        public int AddPassword(AddPasswordDto dto)
        {
            var password = _mapper.Map<Password>(dto);
            _passwordDbContext.Add(password);
            _passwordDbContext.SaveChanges();
            return password.Id;
        }
    }
}
