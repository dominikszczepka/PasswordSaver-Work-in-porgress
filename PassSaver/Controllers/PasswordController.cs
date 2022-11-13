using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Controllers
{
    [Route("password")]
    public class PasswordController : ControllerBase
    {
        private readonly PassSaverDbContext _passwordDbContext;
        private readonly IMapper _mapper;
        public PasswordController(PassSaverDbContext passwordDbContext,IMapper mapper)
        {
            _passwordDbContext = passwordDbContext;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult AddPassword([FromBody] AddPasswordDto dto)
        {
            var password = _mapper.Map<Password>(dto);
            _passwordDbContext.Add(password);
            _passwordDbContext.SaveChanges();
            return Created($"password/{password.Id}",null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<PasswordDto>> GetAll([FromBody] User currentUser)
        {
            var passwords = _passwordDbContext.Passwords.Where(p => p.UserId == currentUser.Id).ToList();
            var passwordsDtos = _mapper.Map<List<PasswordDto>>(passwords);
            return passwordsDtos;
        }
    }
}
