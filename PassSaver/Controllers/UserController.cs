using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PassSaver.Entities;
using PassSaver.Models;
using PassSaver.Services;

namespace PassSaver.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly ILogger<UserServices> _logger;
        private int? _currentUserId;
        public UserController(PassSaverDbContext dbContext, IMapper mapper, ILogger<UserServices> logger,IUserServices userServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userServices = userServices;
            _currentUserId = HttpContext.Session.GetInt32("userId");
        }
        [Route("currentUser")]
        [HttpGet]
        public ActionResult<UserDto> GetCurrent()
        {
            var currentUser = _userServices.GetCurrentUser(_currentUserId);
            return Ok(currentUser);
        }
        [Route("credCheck")]
        [HttpGet]
        public ActionResult<string> AreUserCredentialsTaken([FromBody] AddUserDto dto)
        {
            _userServices.AreUserCredentialsTaken(dto);
            return Ok();
        }
        [Route("userExists")]
        [HttpGet]
        public ActionResult IsUserInTheDB([FromBody] CheckIfUserExistsDto dto)
        {
            _currentUserId = _userServices.IsUserInTheDB(dto);
            HttpContext.Session.SetInt32("userId", (int)_currentUserId);
            return Ok();
        }
        [Route("editUser")]
        [HttpPut]
        public ActionResult EditCurrentUser([FromBody] EditUserDto dto)
        {
            _userServices.EditCurrentUser(_currentUserId, dto);
            return Ok();
        }
        [Route("createUser")]
        [HttpPost]
        public ActionResult CreateUser([FromBody] AddUserDto dto)
        {
            var userId = _userServices.CreateUser(dto);
            return Created($"password/{userId}", null);
        }
    }
}
