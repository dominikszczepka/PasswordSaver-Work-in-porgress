using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [Route("currentUser")]
    public class UserController : ControllerBase
    {
        private readonly PassSaverDbContext _passwordDbContext;
        private readonly IMapper _mapper;
        private readonly UserServices userServices;
        public UserController(PassSaverDbContext passwordDbContext, IMapper mapper)
        {
            _passwordDbContext = passwordDbContext;
            _mapper = mapper;
            userServices= new UserServices(_passwordDbContext, _mapper);
        }

        [HttpGet]
        public ActionResult<UserDto> GetCurrent([FromHeader]int id)
        {
            var currentUser = userServices.GetCurrentUser(id);

            if (currentUser == null)
            {
                return NotFound();
            }
            return Ok(currentUser);
        }
    }
}
