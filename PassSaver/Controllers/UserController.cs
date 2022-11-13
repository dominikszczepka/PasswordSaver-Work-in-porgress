using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Models;

namespace PassSaver.Controllers
{
    [Route("currentUser")]
    public class UserController : ControllerBase
    {
        private readonly PassSaverDbContext _passwordDbContext;
        private readonly IMapper _mapper;
        public UserController(PassSaverDbContext passwordDbContext, IMapper mapper)
        {
            _passwordDbContext = passwordDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserDto> GetCurrent([FromHeader]int id)
        {
            var currentUser = _passwordDbContext.Users
                .Include(p=>p.Passwords)
                .FirstOrDefault(p => p.Id == id);

            if (currentUser == null)
            {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(currentUser);
            return Ok(userDto);
        }
    }
}
