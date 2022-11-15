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
        private readonly PassSaverDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserServices userServices;
        private readonly ILogger<UserServices> _logger;
        public UserController(PassSaverDbContext dbContext, IMapper mapper, ILogger<UserServices> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            userServices = new UserServices(_dbContext, _mapper,_logger);
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
        [HttpGet]
        public ActionResult IsUserOkToAdd([FromBody] AddUserDto dto)
        {
            var msg = userServices.IsUserOkToAdd(dto);
            return msg==null? Ok() :BadRequest(msg) ;
        }
        [HttpPut]
        public ActionResult EditCurrentUser([FromHeader] int id, [FromBody] EditUserDto dto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var isUpdated = userServices.EditCurrentUser(id, dto);
            return isUpdated ? Ok() : NotFound();
        }
        [HttpPost]
        public ActionResult CreateUser([FromBody] AddUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = userServices.CreateUser(dto);
            return Created($"password/{userId}", null);
        }
    }
}
