using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [Route("currentUser")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserServices userServices;
        private readonly ILogger<UserServices> _logger;
        private User _currentUser;
        public UserController(PassSaverDbContext dbContext, IMapper mapper, ILogger<UserServices> logger, User currentUser)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            userServices = new UserServices(_dbContext, _mapper, _logger);
            _currentUser = currentUser;
        }

        [HttpGet]
        public ActionResult<UserDto> GetCurrent()
        {
            var currentUser = userServices.GetCurrentUser(_currentUser.Id);
            return Ok(currentUser);
        }
       [HttpGet]
        public ActionResult<string> AreUserCredentialsTaken([FromBody] AddUserDto dto)
        {
            userServices.AreUserCredentialsTaken(dto);
            return Ok();
        }
        [HttpGet]
        public ActionResult IsUserInTheDB([FromBody] CheckIfUserExistsDto dto)
        {
            _currentUser = userServices.IsUserInTheDB(dto);
            return Ok();
        }
        [HttpPut]
        public ActionResult EditCurrentUser([FromBody] EditUserDto dto)
        {
            userServices.EditCurrentUser(_currentUser.Id, dto);
            return Ok();
        }
        [HttpPost]
        public ActionResult CreateUser([FromBody] AddUserDto dto)
        {
            var userId = userServices.CreateUser(dto);
            return Created($"password/{userId}", null);
        }
    }
}
